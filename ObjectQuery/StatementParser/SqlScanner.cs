using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public class SqlScanner : IScanner
    {
        protected static byte EndOfFileCharacter = 0x7f;

        private WordToken wordToken = new WordToken();
        private NumberToken numberToken = new NumberToken();
        private StringToken stringToken = new StringToken();
        private SpecialToken specialToken = new SpecialToken();
        private EOFToken eofToken = new EOFToken();
        private ErrorToken errorToken = new ErrorToken();

        private TextBuffer buffer;
        private Dictionary<int, Enums.CharCode> charCodeMap = CharCodeMapFactory.GetCharacterMap();

        public SqlScanner(TextBuffer buffer)
        {
            this.buffer = buffer;
        }

        public IToken Get()
        {
            IToken token;
            skipWhiteSpace();

            //--Determine the token class, based on the current character.
            switch (charCodeMap[buffer.CurrentChar])
            {
                case Enums.CharCode.Letter: token = wordToken; break;
                case Enums.CharCode.Digit: token = numberToken; break;
                case Enums.CharCode.Quote: token = stringToken; break;
                case Enums.CharCode.Special: token = specialToken; break;
                case Enums.CharCode.EndOfFile: token = eofToken; break;
                default: token = errorToken; break;
            }

            //--Extract a token of that class, and return a pointer to it.
            token.Get(buffer);
            return token;
        }

        private void skipWhiteSpace()
        {
            char ch = buffer.CurrentChar;

            do
            {
                if (charCodeMap[ch] == Enums.CharCode.WhiteSpace)
                {

                    //--Saw a whitespace character:  fetch the next character.
                    ch = buffer.GetChar();
                }
                else if (ch == '{')
                {
                    //--Skip over a comment, then fetch the next character.
                    do
                    {
                        ch = buffer.GetChar();
                    } while ((ch != '}') && (ch != EndOfFileCharacter));

                    if (ch != EndOfFileCharacter)
                    {
                        ch = buffer.GetChar();
                    }
                    else
                    {
                        //InternalFunctions.Error(ErrorCode.UnexpectedEndOfFile, buffer.CurrentPosition, buffer.ErrorCount++);
                    }
                }
            } while ((charCodeMap[ch] == Enums.CharCode.WhiteSpace));
        }


        #region IScanner Members


        public IToken EOFToken
        {
            get { return eofToken; }
        }

        public IToken ErrorToken
        {
            get { return errorToken; }
        }

        #endregion
    }
}
