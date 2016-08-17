using System;
using System.Collections.Generic;
using System.Text;

namespace SMarTICY
{
    public class PlaylistEntry
    {
        public enum EntryType
        {
            UserSong,
            DirectPath
        }

        private EntryType type;
        private string entryData;

        public string EntryData
        {
            get { return entryData; }
            set { entryData = value; }
        }

        public EntryType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
