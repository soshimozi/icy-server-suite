using System;
using System.Collections.Generic;
using System.Text;

namespace Tracklist.Domain
{
    public class TrackInfo
    {
        private string _url;
        private string _song;
        private string _artist;
        private string _genre;
        private string _album;
        private long _trackId;
        private string _hash;

        public string Hash
        {
            get { return _hash; }
            set { _hash = value; }
        }

        public long TrackId
        {
            get { return _trackId; }
            set { _trackId = value; }
        }

        public string Album
        {
            get { return _album; }
            set { _album = value; }
        }

        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        public string Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }

        public string Song
        {
            get { return _song; }
            set { _song = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
    }
}
