using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using log4net;
using SocketServer;
using ObjectQuery;
using Tracklist.Domain;
using ObjectEventQueue;

namespace SMarTICY
{
    public class ICYServer
    {
        static ILog logger = LogManager.GetLogger(typeof(ICYServer));
        public const string ServerVersion = "2.1.5b";

        private delegate void UserSongHandler(string songId, ref ICYClient client, out PlaylistEntry entry);

        private Mutex clientMutex = new Mutex();

        private bool serverRunning = false;
        private int port;

        //ConnectionManager connectionManager;
        List<ICYClient> clientList = new List<ICYClient>();

        string serverBasePath;

        public ICYServer(string serverBasePath)
        {
            this.serverBasePath = serverBasePath;

            //AsynchObjectEvent<ServerMessage> serverEvent = new AsynchObjectEvent<ServerMessage>();
            //ObjectEventManager.SubscribeEvent<ServerMessage>(serverEvent, new ObjectEventDelegate<ServerMessage>(OnMessage), null);
            //connectionManager = new ConnectionManager();
        }

        public void StartServer(int port)
        {
            if (!serverRunning)
            {
                this.port = port;
                connectionManager.StartServer(port);
                logger.InfoFormat("Server started and listening on port {0}", port);
                serverRunning = true;
            }
        }

        public void StopServer()
        {
            if (serverRunning)
            {
                connectionManager.StopServer();
                serverRunning = false;
            }
        }

        private void OnMessage(string eventName, ServerMessage message)
        {
            switch (message.MessageCode)
            {
                case ServerEvent.ClientConnect:
                    HandleConnect(message);
                    break;

                case ServerEvent.IncommingData:
                    HandleData(message);
                    break;

                case ServerEvent.ClientDisconnect:
                    HandleDisconnect(message);
                    break;

                case ServerEvent.TransmitComplete:
                    HandleTransmitComplete(message);
                    break;
            }
        }

        private void HandleTransmitComplete(ServerMessage message)
        {
            ConnectionProperties properties = (ConnectionProperties)message.MessageData;

            ICYClient client = findClient(properties.DescriptorId);
            if (client != null)
            {
                switch (client.State)
                {
                    case ICYClient.ClientState.Stream:
                        SendNextChunk(client);
                        break;

                    case ICYClient.ClientState.Teardown:
                        break;
                }
            }
        }

        private void SendNextChunk(ICYClient client)
        {
            if (client.IsUsingPlaylist)
            {
                if (client.GetCurrentEntry().Type == PlaylistEntry.EntryType.UserSong)
                {
                    if (!sendDataChunk(client))
                    {
                        // process next playlist entry
                        processPlaylistEntry(client, client.GetNextEntry());
                    }
                }
                else
                {
                    if (client.SongFileStream.Position < client.SongFileStream.Length)
                    {
                        // still reading
                        client.SongFileStream.BeginRead(client.ReadBuffer, 0, ICYClient.ChunkSize, new AsyncCallback(AsynchronousFileRead), client);
                    }
                    else
                    {
                        processPlaylistEntry(client, client.GetNextEntry());
                    }
                }
            }
            else
            {
                // send header data
                //sendMetaData(client, (client.SongFileStream.Position == ICYClient.ChunkSize));

                if (client.SongFileStream.Position < client.SongFileStream.Length)
                {
                    // still reading
                    client.SongFileStream.BeginRead(client.ReadBuffer, 0, ICYClient.ChunkSize, new AsyncCallback(AsynchronousFileRead), client);
                }
                else
                {
                    // done with file
                    client.SongFileStream.Close();
                    connectionManager.Disconnect(client.ClientId);
                    client.State = ICYClient.ClientState.Teardown;
                }
            }
        }

        private void HandleDisconnect(ServerMessage message)
        {
            DisconnectInfo info = (DisconnectInfo)message.MessageData;

            ICYClient client = findClient(info.SocketId);
            if (client != null)
            {
                client.State = ICYClient.ClientState.Disconnect;
                logger.InfoFormat("\n\rClient {0} disconnected from {1}.\n\r{2} bytes sent, {3} bytes received.\n\rTotal time connected {4}ms", client.ClientId, client.ClientAddress, info.BytesSent, info.BytesReceived, info.ConnectedTime.Ticks / TimeSpan.TicksPerMillisecond);
                removeClient(client);
            }
        }

        private void removeClient(ICYClient client)
        {
            if (clientMutex.WaitOne())
            {
                try
                {
                    if (clientList.Contains(client))
                    {
                        clientList.Remove(client);
                    }
                }
                finally
                {
                    clientMutex.ReleaseMutex();
                }
            }
        }

        private void HandleData(ServerMessage message)
        {
            // logging the data
            byte[] data = (byte[])message.MessageData;
            string messageString = ASCIIEncoding.ASCII.GetString(data);

            // find client
            ICYClient client = findClient(message.MessageId);

            if (client != null)
            {
                // data comming in must be header
                logger.InfoFormat("Incomming from Client {0}\n{1}", client.ClientId, messageString);
                if (client.State == ICYClient.ClientState.Connecting)
                {
                    handleNewRequest(client, messageString);
                }
            }
        }

        private void handleNewRequest(ICYClient client, string requestString)
        {
            parseHeader(client, requestString);

            // check for file extension
            string extension = Path.GetExtension(client.Request.Path);

            if (client.Request.HeaderValues.ContainsKey("icy-metadata"))
            {
                // should only be one icy-metadata tag
                if (client.Request.HeaderValues["icy-metadata"][0].Equals("1"))
                {
                    client.SendMetaTag = true;
                }
                else
                {
                    client.SendMetaTag = false;
                }
            }
            else
            {
                client.SendMetaTag = false;
            }

            client.Response.StreamUrl = client.Request.Path;

            string parentDirectory = string.Empty;

            // split path up
            string[] pathElements = client.Request.Path.Replace("\\", "/").Split("/".ToCharArray());
            if (pathElements.Length > 1 && string.IsNullOrEmpty(pathElements[0]))
            {
                // first element is empty, or "root"
                parentDirectory = pathElements[1];
            }
            else if (pathElements.Length > 0)
            {
                parentDirectory = pathElements[0];
            }

            if (parentDirectory.Equals("playlist", StringComparison.OrdinalIgnoreCase))
            {
                // hand a playlist request
                handlePlaylistRequest(client);
            }
            else if (parentDirectory.Equals("track", StringComparison.OrdinalIgnoreCase))
            {
                handleTrackRequest(client);
            }
            //else if (extension.Equals(".mp3", StringComparison.OrdinalIgnoreCase))
            //{
            //    handleFileRequest(client);
            //}
            else
            {
                // only support requests out of the above virtual directories
                sendHttpHeaderFailureResponse(client, "404 Not Found");
                connectionManager.Disconnect(client.ClientId);
            }

        }

        private void handleTrackRequest(ICYClient client)
        {
            TrackInfo trackInfo = getTrackInfo(client);
            if (trackInfo != null)
            {
                string filePath = string.Format("{0}\\{1}", serverBasePath, Path.GetFileName(trackInfo.Url));
                filePath = unescape(filePath.Trim().Replace("\r", ""));

                logger.Info(string.Format("Serving file {0} to client {1}", filePath, client.ClientId));

                if (File.Exists(filePath))
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    long length = fs.Length;
                    fs.Close();

                    List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();

                    headers.Add(new KeyValuePair<string, string>("Content-Length:", length.ToString()));
                    headers.Add(new KeyValuePair<string, string>("Content-Type:", "audio/mpeg"));
                    sendHttpHeaderSuccessResponse(client, headers);

                    // every thing okay, start file
                    startFileTransmit(client, filePath);
                }
                else
                {
                    sendHttpHeaderFailureResponse(client, "404 Not Found");

                    // we are done, close connection on this guy
                    connectionManager.Disconnect(client.ClientId);
                }
            }
            else
            {
                sendHttpHeaderFailureResponse(client, "404 Not Found");
                connectionManager.Disconnect(client.ClientId);
            }
        }

        private void handlePlaylistRequest(ICYClient client)
        {
            UserInfo accountInfo = getAccountInfo(client);

            if (accountInfo != null)
            {
                List<TrackInfo> userTracks = DataAccess.GetTracklist(accountInfo.UserId);

                // get users playlist from datastore
                foreach (TrackInfo song in userTracks)
                {
                    PlaylistEntry entry = new PlaylistEntry();

                    if (song.Url.Contains("?"))
                    {
                        entry.Type = PlaylistEntry.EntryType.UserSong;
                        Dictionary<string, string> arguments = argumentsFromPath(song.Url);
                        if (arguments.ContainsKey("songid"))
                        {
                            entry.EntryData = arguments["songid"];
                        }
                    }
                    else
                    {
                        entry.Type = PlaylistEntry.EntryType.DirectPath;
                        entry.EntryData = string.Format("{0}\\{1}", serverBasePath, Path.GetFileName(song.Url));
                        entry.EntryData = unescape(entry.EntryData.Trim().Replace("\r", ""));
                    }

                    client.AddPlaylistEntry(entry);
                }

                PlaylistEntry firstEntry = client.GetFirstEntry();
                client.IsUsingPlaylist = true;
                
                List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
                //headers.Add(new KeyValuePair<string,string>("Song:", firstEntry.EntryData));
                //headers.Add(new KeyValuePair<string,string>("Server:", string.Format("Zoom Playlist Server v{0}", ServerVersion)));
                sendHttpHeaderSuccessResponse(client, headers);

                processPlaylistEntry(client, firstEntry);
            }
            else
            {
                sendHttpHeaderFailureResponse(client, "404 Not Found");
                connectionManager.Disconnect(client.ClientId);
            }
        }

        private TrackInfo getTrackInfo(ICYClient client)
        {
            TrackInfo result = null;

            string trackIdString = Path.GetFileNameWithoutExtension(client.Request.Path);
            long trackId;
            if( long.TryParse( trackIdString, out trackId ) )
            {
                result = DataAccess.GetTrackInfo(trackId);
            }

            return result;
        }

        private UserInfo getAccountInfo(ICYClient client)
        {
            string userId = string.Empty;

            for (int i = 0; i < client.Request.Arguments.Length; i++)
            {
                string[] nameValue = client.Request.Arguments[i].Split("=".ToCharArray());
                if( nameValue.Length == 2 )
                {
                    if (nameValue[0].Equals("userid", StringComparison.OrdinalIgnoreCase))
                    {
                        userId = nameValue[1];
                        break;
                    }
                }
            }

            Guid userGuid = Guid.Empty;
            try
            {
                userGuid = new Guid(userId);
            }
            catch
            {
            }

            UserInfo result = null;
            if (!userGuid.Equals(Guid.Empty))
            {
                result = DataAccess.GetUserInfoByUserId(userGuid);
            }

            return result;
        }

        private bool sendDataChunk(ICYClient client)
        {
            bool result = false;

            // check if we are beginning of file
            if (client.ChunkCount == 0)
            {
                // now send header info
                //sendMetaData(client, true);
            }
            else
            {
                //sendMetaData(client, false);
            }

            client.ChunkCount++;

            if (client.ChunkCount * ICYClient.ChunkSize < client.SongData.Length)
            {
                // have to chunk the data
                byte[] chunk = new byte[ICYClient.ChunkSize];

                int index = client.ChunkCount * ICYClient.ChunkSize;
                int max = ICYClient.ChunkSize;
                if (index + ICYClient.ChunkSize > client.SongData.Length)
                {
                    max = client.SongData.Length - index;
                }

                Array.Copy(client.SongData, index, chunk, 0, max);

                // send data chunk
                sendServerBytes(client, chunk);

                result = true;
            }

            return result;
        }

        private void processPlaylistEntry(ICYClient client, PlaylistEntry nextEntry)
        {
            if (nextEntry != null)
            {
                if (nextEntry.Type == PlaylistEntry.EntryType.DirectPath)
                {
                    // start first song in playlist
                    startFileTransmit(client, nextEntry.EntryData);
                }
            }
        }

        private Dictionary<string, string> argumentsFromPath(string path)
        {
            string[] pathAndArguments = path.Split("?".ToCharArray());
            Dictionary<string, string> argumentList = new Dictionary<string, string>();

            if (pathAndArguments.Length == 2)
            {
                string [] arguments = pathAndArguments[1].Split("&".ToCharArray());
                for (int i = 0; i < arguments.Length; i++)
                {
                    string[] nameValue = arguments[i].ToLower().Split("=".ToCharArray());
                    if (nameValue.Length == 2)
                    {
                        argumentList.Add(nameValue[0], nameValue[1]);
                    }
                }
            }

            return argumentList;
        }

        private string unescape(string escaped)
        {
            StringBuilder unescaped = new StringBuilder();
            for (int i = 0; i < escaped.Length; i++)
            {
                if (escaped[i] == '%')
                {
                    string hexString = string.Empty;

                    // read next two chars
                    i++;
                    if (i < escaped.Length)
                    {
                        hexString += escaped[i];
                    }

                    i++;
                    if (i < escaped.Length)
                    {
                        hexString += escaped[i];
                    }

                    int value;
                    if (int.TryParse(hexString, System.Globalization.NumberStyles.HexNumber, null, out value))
                    {
                        unescaped.Append((char)value);
                    }
                }
                else if (escaped[i] == '/' && (i < escaped.Length - 1 && escaped[i + 1] == '\''))
                {
                    unescaped.Append("'");
                    i++;
                }
                else
                {
                    unescaped.Append(escaped[i]);
                }
            }

            return unescaped.ToString().Replace("+", " ");
        }

        private void handleFileRequest(ICYClient client)
        {
            if (File.Exists(client.Request.GetServerPath(serverBasePath)))
            {
                FileStream fs = new FileStream(client.Request.GetServerPath(serverBasePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                long length = fs.Length;
                fs.Close();

                List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();

                headers.Add( new KeyValuePair<string,string>("Content-Length:", length.ToString()) );
                headers.Add( new KeyValuePair<string,string>("Content-Type:", "audio/mpeg") );
                sendHttpHeaderSuccessResponse(client, headers);

                // every thing okay, start file
                startFileTransmit(client, client.Request.GetServerPath(serverBasePath));
            }
            else
            {
                sendHttpHeaderFailureResponse(client, "404 Not Found");

                // we are done, close connection on this guy
                connectionManager.Disconnect(client.ClientId);
            }
        }

        //private void sendHeaderResponse(ICYClient client, bool success, long contentLength)
        //{
        //    string badContent = "<html><head></head><body>Page Not Found.</body></html>\n\r";
        //    string okayResponse = "HTTP/1.1 200 OK\r\n";
        //    string badResponse = "HTTP/1.1 404 Not Found\r\n" +
        //                         "Server: SMarTICY/1.0\r\n" +
        //                         "Date: " + DateTime.Now.ToString() + "\r\n" +
        //                         "Content-Type: text/html; charset=utf-8\r\n" +
        //                         "Content-Length: " + badContent.Length.ToString() + "\r\n\r\n" + badContent;

        //    if (success)
        //    {
        //        StringBuilder shoutCastHeader = new StringBuilder();

        //        shoutCastHeader.Append(okayResponse);

        //        shoutCastHeader.Append("Accept-Ranges: bytes\r\n");
        //        shoutCastHeader.AppendFormat("Content-Length: {0}\r\n", contentLength);
        //        shoutCastHeader.Append("Content-Type: audio/mpeg\r\n");
        //        shoutCastHeader.Append("\r\n");
        //        sendServerStringImmediate(client, shoutCastHeader.ToString());
        //    }
        //    else
        //    {
        //        sendServerStringImmediate(client, badResponse);
        //    }
        //}

        private void sendHttpHeaderFailureResponse(ICYClient client, string error)
        {
            string badResponse = string.Format("HTTP/1.1 {0}\r\n", error);
            sendServerStringImmediate(client, badResponse);
        }

        private void sendHttpHeaderSuccessResponse(ICYClient client, List<KeyValuePair<string, string>> extraHeaders)
        {
            string okayResponse = "HTTP/1.1 200 OK\r\n" +
                                  "Accept-Ranges: bytes\r\n" +
                                  "Date: " + DateTime.Now.ToString() + "\r\n";

            StringBuilder shoutCastHeader = new StringBuilder();

            shoutCastHeader.Append(okayResponse);
            foreach (KeyValuePair<string, string> keyValue in extraHeaders)
            {
                shoutCastHeader.Append(string.Format("{0} {1}\r\n", keyValue.Key, keyValue.Value));
            }

            shoutCastHeader.Append("\r\n");
            sendServerStringImmediate(client, shoutCastHeader.ToString());
        }

        private void startFileTransmit(ICYClient client, string filePath)
        {
            // tell state machine to send header next time through for this client
            client.State = ICYClient.ClientState.Stream;

            MP3Info.ID3v1HeaderReader reader = new MP3Info.ID3v1HeaderReader();
            reader.OnDecodeComplete += new MP3Info.ID3v1HeaderReader.DecodeCompleteHandler(headerReader_OnDecodeComplete);
            reader.BeginDecode(filePath, client);
        }

        void headerReader_OnDecodeComplete(object sender, MP3Info.DecodeEventArgs args)
        {
            ICYClient client = (ICYClient)args.UserState;

            UpdateStreamInfo(args.Header, client);

            string filePath = string.Empty;
            if (client.IsUsingPlaylist)
            {
                PlaylistEntry currentEntry = client.GetCurrentEntry();
                if (currentEntry != null)
                {
                    filePath = currentEntry.EntryData;
                }
            }
            else
            {
                // transmitting a single file
                filePath = client.Request.GetServerPath(serverBasePath);

                string parentDirectory = string.Empty;

                // split path up
                string[] pathElements = client.Request.Path.Replace("\\", "/").Split("/".ToCharArray());
                if (pathElements.Length > 1 && string.IsNullOrEmpty(pathElements[0]))
                {
                    // first element is empty, or "root"
                    parentDirectory = pathElements[1];
                }
                else if (pathElements.Length > 0)
                {
                    parentDirectory = pathElements[0];
                }

                if (parentDirectory.Equals("track", StringComparison.OrdinalIgnoreCase))
                {
                    TrackInfo track = getTrackInfo(client);
                    if (track != null)
                    {
                        filePath = string.Format("{0}\\{1}", serverBasePath, Path.GetFileName(track.Url));
                        filePath = unescape(filePath.Trim().Replace("\r", ""));
                    }
                }

            }

            try
            {
                client.SongFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                client.SongFileStream.BeginRead(client.ReadBuffer, 0, ICYClient.ChunkSize, new AsyncCallback(AsynchronousFileRead), client);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                connectionManager.Disconnect(client.ClientId);
            }

        }

        private static void UpdateStreamInfo(MP3Info.ID3v1Header header, ICYClient client)
        {
            StringBuilder streamName = new StringBuilder();
            if (!string.IsNullOrEmpty(header.Artist))
            {
                streamName.Append(header.Artist);
            }

            if (!string.IsNullOrEmpty(header.Title))
            {
                if (streamName.Length > 0)
                {
                    streamName.Append(" - ");                
                }

                streamName.Append(header.Title);
            }

            if (streamName.Length == 0)
            {
                streamName.Append("Unknown Artist");
            }
            client.Response.StreamName = streamName.ToString();
        }

        private void sendServerStringImmediate(ICYClient client, string data)
        {
            ServerMessage message = new ServerMessage();

            message.MessageCode = ServerEvent.OutgoingData;
            message.MessageId = client.ClientId;
            message.MessageData = ASCIIEncoding.ASCII.GetBytes(data);

            connectionManager.SendData(client.ClientId, (byte [])message.MessageData, false);
        }

        private void sendServerString(ICYClient client, string data)
        {

            ServerMessage message = new ServerMessage();

            message.MessageCode = ServerEvent.OutgoingData;
            message.MessageId = client.ClientId;
            message.MessageData = ASCIIEncoding.ASCII.GetBytes(data);

            connectionManager.SendData(client.ClientId, (byte [])message.MessageData, true);
        }
            
        private void sendServerBytes(ICYClient client, byte [] data)
        {
            ServerMessage message = new ServerMessage();

            message.MessageCode = ServerEvent.OutgoingData;
            message.MessageId = client.ClientId;
            message.MessageData = data;

            connectionManager.SendData(client.ClientId, data, true);
        }

        private void sendServerBytesImmediate(ICYClient client, byte[] data)
        {
            ServerMessage message = new ServerMessage();

            message.MessageCode = ServerEvent.OutgoingData;
            message.MessageId = client.ClientId;
            message.MessageData = data;

            connectionManager.SendData(client.ClientId, data, false);
        }

        private void AsynchronousFileRead(IAsyncResult result)
        {
            ICYClient client = (ICYClient)result.AsyncState;

            // end asynchronous operation
            int bytes = client.SongFileStream.EndRead(result);

            byte[] data = new byte[bytes];
            Array.Copy(client.ReadBuffer, data, bytes);
            client.ClearRecieveBuffer();

            sendServerBytes(client, data);

        }
        

        //private void sendMetaData(ICYClient client, bool forceUpdate)
        //{
        //    byte [] data;
        //    byte[] asciiData;
        //    byte headerLength;
        //    StringBuilder metaHeader = new StringBuilder();

        //    if (client.SendMetaTag)
        //    {
        //        if (forceUpdate)
        //        {
        //            // now send header info
        //            metaHeader.AppendFormat("StreamTitle='{0}';StreamUrl='{1}';", client.Response.StreamName, client.Response.StreamUrl);
        //            asciiData = ASCIIEncoding.ASCII.GetBytes(metaHeader.ToString());
        //            headerLength = (byte)(metaHeader.Length / 16);

        //            if (metaHeader.Length % 16 != 0)
        //            {
        //                headerLength++;
        //            }

        //            data = new byte[(headerLength * 16) + 1];

        //            data[0] = headerLength;

        //            Array.Copy(asciiData, 0, data, 1, asciiData.Length);

        //            // header sent, reset state
        //            client.State = ICYClient.ClientState.Stream;
        //        }
        //        else
        //        {
        //            data = new byte[1];
        //            data[0] = 0;
        //        }

        //        sendServerBytesImmediate(client, data);
        //    }
        //}

        private void parseHeader(ICYClient client, string headerString)
        {
            // first break up by cr/lf
            string [] headerLines = headerString.Split("\n".ToCharArray());

            client.Request.HeaderValues.Clear();
            client.Request.Action = string.Empty;
            client.Request.Path = string.Empty;

            // there better be at least a get
            if (headerLines.Length > 0)
            {
                // split first element by spaces
                string [] actionSections = headerLines[0].Split(" ".ToCharArray());

                // should be 3 sections GET <resource> <protocol>
                if (actionSections.Length > 0)
                {
                    client.Request.Action = actionSections[0].Trim().Replace("\r", "");
                }

                if (actionSections.Length > 1)
                {
                    string[] pathAndArguments = actionSections[1].Split("?".ToCharArray());

                    if (pathAndArguments.Length > 0)
                    {
                        client.Request.Path = unescape(pathAndArguments[0].Trim().Replace("\r", ""));
                    }

                    if (pathAndArguments.Length > 1)
                    {
                        client.Request.Arguments = pathAndArguments[1].Split("&".ToCharArray());
                    }
                }

                if (actionSections.Length > 2)
                {
                    client.Request.Protocol = actionSections[2];
                }

            }

            for (int i = 1; i < headerLines.Length; i++)
            {
                // get name value pairs
                string[] nameValuePair = headerLines[i].Split(":".ToCharArray());

                string name = string.Empty;
                string value = string.Empty;
                if (nameValuePair.Length > 0)
                {
                    name = nameValuePair[0].Trim().Replace("\r", "").Trim().ToLower();
                }

                if (nameValuePair.Length > 1)
                {
                    value = nameValuePair[1].Trim().Replace("\r", "").Trim();
                }

                if (!string.IsNullOrEmpty(name))
                {
                    if (!client.Request.HeaderValues.ContainsKey(name))
                    {
                        client.Request.HeaderValues.Add(name, new List<string>());
                    }

                    client.Request.HeaderValues[name].Add(value);
                }
            }
        }

        private ICYClient findClient(long messageId)
        {
            ICYClient client = null;
            if (clientMutex.WaitOne())
            {
                try
                {
                    string sql = "from ICYClient as c where c.ClientId = ?";
                    IQuery<ICYClient> query = QueryManager<ICYClient>.GetQuery(sql, messageId);
                    client = query.FindOne(clientList);
                }
                finally
                {
                    clientMutex.ReleaseMutex();
                }
            }

            return client;
        }

        private void HandleConnect(ServerMessage message)
        {
            ConnectionProperties properties = (ConnectionProperties)message.MessageData;

            ICYClient client = new ICYClient();
            client.State = ICYClient.ClientState.Connecting;
            client.ClientAddress = properties.RemoteAddress;
            client.ClientId = properties.DescriptorId;
            addClient(client);
            
            logger.InfoFormat("Client {0} connected from {1}.", client.ClientId, client.ClientAddress);
        }

        private void addClient(ICYClient client)
        {
            if (clientMutex.WaitOne())
            {
                try
                {
                    clientList.Add(client);
                }
                finally
                {
                    clientMutex.ReleaseMutex();
                }
            }
        }

        //private void ServerThread()
        //{
        //    connectionManager.StartServer(port);

        //    Console.WriteLine("Server started and listening on port {0}", port);

        //    // serve connections and data
        //    while (!stopEvent.WaitOne(1, false))
        //    {
        //        connectionManager.Serve();
        //    }
        //}
    }
}
