using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public class NumberToken : IToken
    {
        const int maxInteger = 32767;
        const int maxExponent = 37;
        const int maxDigitCount = 20;

        int digitCount = 0;      // total no. of digits in number
        bool countError = false;  // true if too many digits, else false

        Enums.TokenCode tokenCode = Enums.TokenCode.Dummy;
        DataType type = new DataType();
        DataValue dataValue = new DataValue();
        string tokenString = string.Empty;

        public void Get(IInputBuffer buffer)
        {
            StringBuilder tokenBuilder = new StringBuilder();

            char ch;

            double numValue = 0.0;           // value of number ignoring
                                            //    the decimal point
            int wholePlaces = 0;            // no. digits before the decimal point
            int decimalPlaces = 0;          // no. digits after  the decimal point
            char exponentSign = '+';
            double eValue = 0.0;             // value of number after 'E'
            int exponent = 0;               // final value of exponent
            bool sawDotDotFlag = false;     // true if encountered '..',
                                            //   else false

            ch = buffer.CurrentChar;
            digitCount = 0;
            countError = false;

            Code = Enums.TokenCode.Error;    // we don't know what it is yet, but
            Type = DataType.IntegerType;  //    assume it'll be an integer

            //--Get the whole part of the number by accumulating
            //--the values of its digits into numValue.  wholePlaces keeps
            //--track of the number of digits in this part.
            if (accumulateValue(buffer, tokenBuilder, ref numValue))
            {
                wholePlaces = digitCount;

                ch = buffer.CurrentChar;

                ////--If the current character is a dot, then either we have a
                ////--fraction part or we are seeing the first character of a '..'
                ////--token.  To find out, we must fetch the next character.
                if (ch == '.')
                {
                    ch = buffer.GetChar();

                    if (ch == '.')
                    {

                        //--We have a .. token.  Back up bufferp so that the
                        //--token can be extracted next.
                        sawDotDotFlag = true;
                        buffer.PutBackChar();
                    }
                    else
                    {
                        Type = DataType.RealType;
                        tokenBuilder.Append(".");

                        //--We have a fraction part.  Accumulate it into numValue.
                        if (!accumulateValue(buffer, tokenBuilder, ref numValue)) return;

                        // get the current character
                        ch = buffer.CurrentChar;

                        decimalPlaces = digitCount - wholePlaces;
                    }
                }

                //--Get the exponent part, if any. There cannot be an
                //--exponent part if we already saw the '..' token.
                if (!sawDotDotFlag && ((ch == 'E') || (ch == 'e')))
                {
                    Type = DataType.RealType;
                    tokenBuilder.Append(ch);
                    ch = buffer.GetChar();

                    //--Fetch the exponent's sign, if any.
                    if ((ch == '+') || (ch == '-'))
                    {
                        exponentSign = ch;
                        tokenBuilder.Append(ch);
                        ch = buffer.GetChar();
                    }

                    //--Accumulate the value of the number after 'E' into eValue.
                    digitCount = 0;
                    if (!accumulateValue(buffer, tokenBuilder, ref eValue)) return;

                    ch = buffer.CurrentChar;
                    if (exponentSign == '-') eValue = -eValue;
                }

                //--Were there too many digits?
                if (countError) 
                {
                    return;
                }

                //--Calculate and check the final exponent value,
                //--and then use it to adjust the number's value.
                exponent = (int)eValue - decimalPlaces;
                if ((exponent + wholePlaces < -maxExponent) ||
                    (exponent + wholePlaces >  maxExponent)) 
                {
                    return;
                }
                
                if (exponent != 0)
                {
                    numValue *= (double)Math.Pow(10.0, exponent);
                }

                //--Check and set the numeric value.
                if (Type == DataType.IntegerType) 
                {
                    if ((numValue < -maxInteger) || (numValue > maxInteger)) 
                    {
                        return;
                    }
                
                    TokenValue.IntegerValue = (int)numValue;
                }
                else 
                {
                    TokenValue.RealValue = numValue;
                }

                TokenString = tokenBuilder.ToString();
                Code = Enums.TokenCode.Number;
            }
        }

        public void Print(IOutputBuffer buffer)
        {
            string lineText;
            if (Type == DataType.IntegerType)
            {
                lineText = string.Format("\t{0, -18} {1}", ">> integer:", TokenValue.IntegerValue);
            }
            else
            {
                lineText = string.Format("\t{0, -18} {1:g}", ">> real:", TokenValue.RealValue);
            }

            buffer.PutLine(lineText);
        }

        private bool accumulateValue(IInputBuffer buffer, StringBuilder tokenBuilder, ref double value)
        {
            char ch = buffer.CurrentChar;
            Dictionary<int, Enums.CharCode> charCodeMap = CharCodeMapFactory.GetCharacterMap();

            //--Error if the first character is not a digit.
            if (charCodeMap[ch] != Enums.CharCode.Digit)
            {
                return false;           // failure
            }

            //--Accumulate the value as long as the total allowable
            //--number of digits has not been exceeded.
            do
            {
                tokenBuilder.Append(ch);

                if (++digitCount <= maxDigitCount)
                {
                    value = 10 * value + (ch - '0');  // shift left and add
                }
                else countError = true;         // too many digits, but keep reading anyway

                ch = buffer.GetChar();
            } while (charCodeMap[ch] == Enums.CharCode.Digit);

            return true;               // success
        }

        #region IToken Members

        public bool IsDelimiter
        {
            get { return false; }
        }

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

        public bool IsReservedWord
        {
            get { return false; }
        }

        #endregion
    }
}
