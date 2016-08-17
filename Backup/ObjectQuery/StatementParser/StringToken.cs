using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    class StringToken : IToken
    {
        protected static byte EndOfFileCharacter = 0x7f;
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
            char ch;           // current character
            StringBuilder tokenBuilder = new StringBuilder();

            tokenBuilder.Append('\'');  // opening quote

            //--Get the string.
            ch = buffer.GetChar();  // first char after opening quote
            while (ch != EndOfFileCharacter)
            {
                if (ch == '\'')
                {     // look for another quote

                    //--Fetched a quote.  Now check for an adjacent quote,
                    //--since two consecutive quotes represent a single
                    //--quote in the string.
                    ch = buffer.GetChar();
                    if (ch != '\'') break;  // not another quote, so previous
                    //   quote ended the string
                }
                //--Replace the end of line character with a blank.
                else if (ch == '\0') ch = ' ';

                //--Append current char to string, then get the next char.
                tokenBuilder.Append(ch);
                ch = buffer.GetChar();
            }

            tokenBuilder.Append('\'');  // closing quote
            TokenString = tokenBuilder.ToString();
            Code = Enums.TokenCode.String;
        }

        public void Print(IOutputBuffer buffer)
        {
            string lineText = string.Format("\t{0, -18} {1}", ">> string:", TokenString);
            buffer.PutLine(lineText);
        }

        public bool IsDelimiter
        {
            get { return true; }
        }

        public bool IsReservedWord
        {
            get { return false; }
        }

    }
}
