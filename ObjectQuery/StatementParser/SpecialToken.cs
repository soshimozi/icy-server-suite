using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    class SpecialToken : IToken
    {
        #region IToken Members
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
            char ch = buffer.CurrentChar;
            StringBuilder tokenBuilder = new StringBuilder();

            tokenBuilder.Append(ch);

            switch (ch)
            {
                case '(': Code = Enums.TokenCode.LParen; buffer.GetChar(); break;
                case ')': Code = Enums.TokenCode.RParen; buffer.GetChar(); break;
                case '=': Code = Enums.TokenCode.Equal; buffer.GetChar(); break;
                case '[': Code = Enums.TokenCode.LBracket; buffer.GetChar(); break;
                case ']': Code = Enums.TokenCode.RBracket; buffer.GetChar(); break;
                case ';': Code = Enums.TokenCode.Semicolon; buffer.GetChar(); break;
                case ',': Code = Enums.TokenCode.Comma; buffer.GetChar(); break;
                case '?': Code = Enums.TokenCode.Question; buffer.GetChar(); break;
                case '!':
                    ch = buffer.GetChar();
                    if (ch == '=')
                    {
                        Code = Enums.TokenCode.Ne;
                        buffer.GetChar();
                    }
                    break;

                case '<':
                    ch = buffer.GetChar();     // < or <= or <>
                    if (ch == '=')
                    {
                        tokenBuilder.Append('=');
                        Code = Enums.TokenCode.Le;
                        buffer.GetChar();
                    }
                    else
                    {
                        Code = Enums.TokenCode.Lt;
                    }
                    break;

                case '>':
                    ch = buffer.GetChar();     // > or >=
                    if (ch == '=')
                    {
                        tokenBuilder.Append('=');
                        Code = Enums.TokenCode.Ge;
                        buffer.GetChar();
                    }
                    else
                    {
                        Code = Enums.TokenCode.Gt;
                    }
                    break;

                case '.':
                    Code = Enums.TokenCode.Period;
                    buffer.GetChar();    
                    break;

                default:
                    Code = Enums.TokenCode.Error;                  // error
                    buffer.GetChar();
                    break;
            }

            TokenString = tokenBuilder.ToString();
        }

        public bool IsDelimiter
        {
            get { return true; }
        }

        public void Print(IOutputBuffer buffer)
        {
            string lineText = string.Format("\t{0, -18} {1}", ">> special:", TokenString);
            buffer.PutLine(lineText);
        }

        public bool IsReservedWord
        {
            get { return false; }
        }


        #endregion
    }
}
