using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SMarTICY
{
    class ICYClient
    {
        public const int ChunkSize = 1024*24;

        public enum ClientState
        {
            Connecting,
            Stream,
            Teardown,
            Disconnect
        }
        ClientState state;

        Response response = new Response();
        Request request = new Request();

        long clientId;
        string clientAddress;

        FileStream stream;
        byte[] readBuffer = new byte[ChunkSize];

        bool sendMetaTag = false;

        List<PlaylistEntry> playlist = new List<PlaylistEntry>();
        
        bool usingPlaylist = false;
        int currentListEntry = 0;

        byte[] songData;
        int chunkCount;

        public int ChunkCount
        {
            get { return chunkCount; }
            set { chunkCount = value; }
        }

        public byte[] SongData
        {
            get { return songData; }
            set { songData = value; }
        }

        public int PlaylistEntryCount
        {
            get { return playlist.Count; }
        }

        public void AddPlaylistEntry(PlaylistEntry entry)
        {
            playlist.Add(entry);
        }

        public void RemovePlaylistEntry(PlaylistEntry entry)
        {
            playlist.Remove(entry);
        }

        public void ClearPlaylist()
        {
            playlist.Clear();
        }

        public PlaylistEntry GetCurrentEntry()
        {
            PlaylistEntry currentEntry = null;
            if (currentListEntry < playlist.Count)
            {
                currentEntry = playlist[currentListEntry];
            }
            return currentEntry;
        }

        public PlaylistEntry GetNextEntry()
        {
            PlaylistEntry nextEntry = null;
            if (++currentListEntry < playlist.Count)
            {
                nextEntry = playlist[currentListEntry];
            }
            else if (playlist.Count > 0)
            {
                // wrap around
                currentListEntry = 0;
                nextEntry = playlist[currentListEntry];
            }
            return nextEntry;
        }

        public bool IsUsingPlaylist
        {
            get { return usingPlaylist; }
            set { usingPlaylist = value; }
        }

        public bool SendMetaTag
        {
            get { return sendMetaTag; }
            set { sendMetaTag = value; }
        }

        public Response Response
        {
            get { return response; }
            set { response = value; }
        }

        public byte[] ReadBuffer
        {
            get { return readBuffer; }
        }

        public FileStream SongFileStream
        {
            get { return stream; }
            set { stream = value; }
        }

        public Request Request
        {
            get { return request; }
        }

        public ClientState State
        {
            get { return state; }
            set { state = value; }
        }

        public string ClientAddress
        {
            get { return clientAddress; }
            set { clientAddress = value; }
        }

        public long ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        internal void ClearRecieveBuffer()
        {
            readBuffer = new byte[ChunkSize];
        }

        internal PlaylistEntry GetFirstEntry()
        {
            currentListEntry = 0;
            if( playlist.Count > 0)
            {
                return playlist[0];
            }
            else
            {
                return null;
            }
        }
    }
}
