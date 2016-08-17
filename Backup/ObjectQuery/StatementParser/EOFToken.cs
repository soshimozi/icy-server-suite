using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public class EOFToken : IToken
    {
        #region IToken Members

        public void Get(IInputBuffer buffer)
        {
        }

        public void Print(IOutputBuffer buffer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool IsDelimiter
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public Enums.TokenCode Code
        {
            get
            {
                return Enums.TokenCode.EndOfFile;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public DataType Type
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public DataValue TokenValue
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public string TokenString
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public bool IsReservedWord
        {
            get { return false; }
        }
        #endregion
    }
}
