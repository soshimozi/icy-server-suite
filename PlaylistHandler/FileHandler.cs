using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using Tracklist.Domain;


namespace PlaylistHandler
{
    public class FileHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private const string PlaylistRequestFile = "playlist.flv";
        private const string PlaylistRequestFileV2 = "playlist.xml";

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session.IsNewSession) 
            {
                if (PlaylistRequestFile == System.IO.Path.GetFileName(context.Request.Path).ToLower())
                {
                    if (context.Request.QueryString["userid"] != null)
                    {
                        Guid userId;

                        try
                        {
                            userId = new Guid(context.Request.QueryString["userid"]);
                            
                            // get tracks for this user
                            List<TrackInfo> trackList = DataAccess.GetTracklist(userId);

                            sendTrackListResponse(context.Response, trackList);
                        }
                        catch
                        {
                            context.Response.StatusCode = 500;
                        }

                    }
                }
                else if (PlaylistRequestFileV2 == System.IO.Path.GetFileName(context.Request.Path).ToLower())
                {
                    if (context.Request.QueryString["userid"] != null)
                    {
                        Guid userId;

                        try
                        {
                            userId = new Guid(context.Request.QueryString["userid"]);

                            // get tracks for this user
                            List<TrackInfo> trackList = DataAccess.GetTracklist(userId);

                            sendTrackListResponseV2(context.Response, trackList);
                        }
                        catch
                        {
                            context.Response.StatusCode = 500;
                        }


                    }
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
        }

        private void sendTrackListResponseV2(HttpResponse httpResponse, List<TrackInfo> trackList)
        {
            XmlDocument playlistDocument = new XmlDocument();

            XmlElement playlistNode = playlistDocument.CreateElement("playlist");
            playlistNode.Attributes.Append(playlistDocument.CreateAttribute("version"));
            playlistNode.Attributes["version"].Value = "1";

            playlistNode.Attributes.Append(playlistDocument.CreateAttribute("xmlns"));
            playlistNode.Attributes["xmlns"].Value = "http://xspf.org/ns/0/";

            XmlElement tracklistNode = playlistDocument.CreateElement("trackList");
            playlistNode.AppendChild(tracklistNode);

            playlistDocument.AppendChild(playlistNode);

            for (int i = 0; i < trackList.Count; i++)
            {
                XmlElement trackNode = playlistDocument.CreateElement("track");

                XmlElement childNode = playlistDocument.CreateElement("title");
                childNode.InnerText = trackList[i].Song;
                trackNode.AppendChild(childNode);

                childNode = playlistDocument.CreateElement("creator");
                childNode.InnerText = trackList[i].Artist;
                trackNode.AppendChild(childNode);


                childNode = playlistDocument.CreateElement("location");
                childNode.InnerText = trackList[i].Url.Replace("'", "%27");
                trackNode.AppendChild(childNode);

                tracklistNode.AppendChild(trackNode);
            }

            httpResponse.ContentType = "text/xml";
            playlistDocument.Save(httpResponse.OutputStream);
        }

        private void sendTrackListResponse(HttpResponse httpResponse, List<TrackInfo> trackList)
        {
            XmlDocument songlistDocument = new XmlDocument();

            XmlElement songsNode = songlistDocument.CreateElement("songs");
            songlistDocument.AppendChild(songsNode);

            for (int i = 0; i < trackList.Count; i++)
            {
                XmlElement songNode = songlistDocument.CreateElement("song");
                songNode.Attributes.Append(songlistDocument.CreateAttribute("url"));
                songNode.Attributes.Append(songlistDocument.CreateAttribute("track"));
                songNode.Attributes.Append(songlistDocument.CreateAttribute("artist"));

                songNode.Attributes["track"].Value = trackList[i].Song;
                songNode.Attributes["artist"].Value = trackList[i].Artist;
                songNode.Attributes["url"].Value = trackList[i].Url.Replace("'", "%27");
                songsNode.AppendChild(songNode);
            }

            httpResponse.ContentType = "text/xml";
            songlistDocument.Save(httpResponse.OutputStream);
        }

        #endregion
    }
}
