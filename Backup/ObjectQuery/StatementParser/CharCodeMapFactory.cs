using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    static class CharCodeMapFactory
    {
        static byte EndOfFileCharacter = 0x7f;
        static Dictionary<int, Enums.CharCode> charCodeMap;
        static CharCodeMapFactory()
        {
            charCodeMap = new Dictionary<int, Enums.CharCode>();
            InitalizeValues();
        }

        public static Dictionary<int, Enums.CharCode> GetCharacterMap()
        {
            return charCodeMap;
        }

        private static void InitalizeValues()
        {
            int i;

            //--Initialize the character code map.
            for (i = 0; i < 128; ++i) charCodeMap[i] = Enums.CharCode.Error;
            for (i = 'a'; i <= 'z'; ++i) charCodeMap[i] = Enums.CharCode.Letter;
            for (i = 'A'; i <= 'Z'; ++i) charCodeMap[i] = Enums.CharCode.Letter;
            for (i = '0'; i <= '9'; ++i) charCodeMap[i] = Enums.CharCode.Digit;
            charCodeMap['?'] = Enums.CharCode.Special;
            charCodeMap['+'] = charCodeMap['-'] = Enums.CharCode.Special;
            charCodeMap['*'] = charCodeMap['/'] = Enums.CharCode.Special;
            charCodeMap['='] = charCodeMap['^'] = Enums.CharCode.Special;
            charCodeMap['.'] = charCodeMap[','] = Enums.CharCode.Special;
            charCodeMap['<'] = charCodeMap['>'] = Enums.CharCode.Special;
            charCodeMap['('] = charCodeMap[')'] = Enums.CharCode.Special;
            charCodeMap['['] = charCodeMap[']'] = Enums.CharCode.Special;
            charCodeMap['{'] = charCodeMap['}'] = Enums.CharCode.Special;
            charCodeMap[':'] = charCodeMap[';'] = Enums.CharCode.Special;
            charCodeMap[' '] = charCodeMap['\t'] = Enums.CharCode.WhiteSpace;
            charCodeMap['\n'] = charCodeMap['\0'] = Enums.CharCode.WhiteSpace;
            charCodeMap['\''] = Enums.CharCode.Quote;
            charCodeMap[EndOfFileCharacter] = Enums.CharCode.EndOfFile;

        }
    }
}
