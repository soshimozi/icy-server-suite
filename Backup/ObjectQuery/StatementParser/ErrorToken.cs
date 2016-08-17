using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    class ErrorToken : IToken
    {
        Enums.TokenCode tokenCode = Enums.TokenCode.Dummy;
        DataType type = new DataType();
        DataValue dataValue = new DataValue();
        string tokenString = string.Empty;

        public Enums.TokenCode Code
        {
            get { return tokenCode; }
            set { tokenCode = value; }
        }

        public DataType Type
        {
            get { return type; }
            set { type = value; }
        }

        public DataValue TokenValue
        {
            get { return dataValue; }
            set { dataValue = value; }
        }

        public string TokenString
        {
            get { return tokenString; }
            set { tokenString = value; }
        }

        public void Get(IInputBuffer buffer)
        {
            StringBuilder tokenBuilder = new StringBuilder();
            tokenBuilder.Append(buffer.CurrentChar);
            TokenString = tokenBuilder.ToString();

            buffer.GetChar();
        }

        public bool IsDelimiter
        {
            get { return false; }
        }

        public void Print(IOutputBuffer buffer)
        {
        }

        public bool IsReservedWord
        {
            get { return false; }
        }


    }
}
