using System;
using System.Collections.Generic;
using System.Text;

namespace MP3Info
{
    public class ID3v1Header
    {
        string title = string.Empty;
        string artist = string.Empty;
        string album = string.Empty;
        string year = string.Empty;
        string comment = string.Empty;
        int genreID = 0;
        int track = 0;
        bool hasTag = false;

        private ID3v1Header() { }

        public ID3v1Header(byte[] buff)
        {
            decodeBuffer(buff);
        }

        public bool HasTag
        {
            get { return hasTag; }
            set { hasTag = value; }
        }

        public string Title
        {
            get { return title; }
        }

        public string Artist
        {
            get { return artist; }
        }

        public string Album
        {
            get { return album; }
        }

        public string Year
        {
            get { return year; }
        }

        public string Comment
        {
            get { return comment; }
        }

        public int GenreID
        {
            get { return genreID; }
        }

        public int Track
        {
            get { return track; }
        }

        private void decodeBuffer(byte [] data)
        {
            // Convert the Byte Array to a String
            Encoding instEncoding = new ASCIIEncoding();
            string id3Tag = Encoding.ASCII.GetString(data);

            // If there is an attched ID3 v1.x TAG then read it 
            if (id3Tag.Substring(0, 3) == "TAG")
            {
                title = CleanTag(id3Tag.Substring(3, 30).Trim());
                artist = CleanTag(id3Tag.Substring(33, 30).Trim());
                album = CleanTag(id3Tag.Substring(63, 30).Trim());
                year = CleanTag(id3Tag.Substring(93, 4).Trim());
                comment = CleanTag(id3Tag.Substring(97, 28).Trim());

                // Get the track number if TAG conforms to ID3 v1.1
                if (id3Tag[125] == 0)
                    track = data[126];
                else
                    track = 0;
                genreID = data[127];

                hasTag = true;
            }
            else
            {
                hasTag = false;
            }
        }

        private string CleanTag(string tagValue)
        {
            if (tagValue.IndexOf('\0') != -1)
            {
                return tagValue.Substring(0, tagValue.IndexOf('\0'));
            }

            return tagValue;
        }

    }
}
