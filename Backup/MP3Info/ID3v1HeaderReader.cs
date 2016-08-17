/// <summary>
/// Summary description for IPv4
/// </summary>
using System;
using System.IO;
using System.Text;

namespace MP3Info
{
    public class ID3v1HeaderReader
    {
        public delegate void DecodeCompleteHandler(object sender, DecodeEventArgs args);

        public ID3v1HeaderReader() { }

        public ID3v1Header Decode(string fileName)
        {
            ID3v1Header header = null;

            byte [] buffer = new byte[128];
            if (File.Exists(fileName))
            {
                // Read the 128 byte ID3 tag into a byte array
                FileStream stream = null;
                try
                {
                    // Read the 128 byte ID3 tag into a byte array
                    stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    stream.Seek(-128, SeekOrigin.End);
                    stream.Read(buffer, 0, 128);
                }
                catch { }
                finally
                {
                    stream.Close();
                }

                header = new ID3v1Header(buffer);
            }

            return header;
        }

        public bool BeginDecode(string fileName, object state)
        {
            if (File.Exists(fileName))
            {
                FileStream stream = null;
                try
                {
                    // Read the 128 byte ID3 tag into a byte array
                    stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    stream.Seek(-128, SeekOrigin.End);
                }
                catch { }

                if (stream != null)
                {
                    AsynchStateObject stateObject = new AsynchStateObject();
                    stateObject.Stream = stream;
                    stateObject.State = state;
                    stream.BeginRead(stateObject.HeaderBuffer, 0, AsynchStateObject.HeaderSize, new AsyncCallback(readCallback), stateObject);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public event DecodeCompleteHandler OnDecodeComplete = null;

        private void readCallback(IAsyncResult result)
        {
            AsynchStateObject stateObject = (AsynchStateObject)result.AsyncState;
            if (result.IsCompleted)
            {
                stateObject.Stream.Close();

                ID3v1Header header = new ID3v1Header(stateObject.HeaderBuffer);
                fireOnDecodeComplete(header, stateObject.State);
            }
        }

        private void fireOnDecodeComplete(ID3v1Header header, object state)
        {
            if (OnDecodeComplete != null)
            {
                OnDecodeComplete(null, new DecodeEventArgs(header, state));
            }
        }
    }
}

