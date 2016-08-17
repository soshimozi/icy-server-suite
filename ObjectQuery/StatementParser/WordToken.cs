using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public class WordToken : IToken
    {
        //const int minResWordLen = 2;    // min and max reserved
        //const int maxResWordLen = 9;    //   word lengths

        Dictionary<int, Enums.CharCode> characterMap = CharCodeMapFactory.GetCharacterMap();

        Enums.TokenCode tokenCode = Enums.TokenCode.Dummy;
        DataType type = new DataType();
        DataValue dataValue = new DataValue();
        string tokenString = string.Empty;

        bool reservedWord = false;

        public Enums.TokenCode Code
        {
            get { return tokenCode; }
            set { tokenCode = value; }
        }

        public bool IsReservedWord
        {
            get { return reservedWord; }
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
            char ch = buffer.CurrentChar;  // char fetched from input
            do
            {
                tokenBuilder.Append(ch);
                ch = buffer.GetChar();
            } while (characterMap[ch] == Enums.CharCode.Letter ||
                        characterMap[ch] == Enums.CharCode.Digit);

            TokenString = tokenBuilder.ToString();
            checkForReservedWord();
        }


        public void Print(IOutputBuffer buffer)
        {
            string line;
            if (Code == Enums.TokenCode.Identifier)
            {
                line = string.Format("\t{0,-18} {1}", ">> identifier:", TokenString);
            }
            else
            {
                line = string.Format("\t{0, -18} {1}", ">> reserved word:", TokenString);
            }
            buffer.PutLine(line);
        }

        private void checkForReservedWord()
        {
            int len = TokenString.Length;

            Code = Enums.TokenCode.Identifier;  // first assume it's an identifier
            ReservedWord rw = ReservedWordDictionary.FindReservedWord(TokenString);
            if (rw != null)
            {
                Code = rw.Code;
                reservedWord = true;
            }
            else
            {
                reservedWord = false;
            }
        }


        #region IToken Members


        bool IToken.IsDelimiter
        {
            get { return false; }
        }

        #endregion
    }

}
