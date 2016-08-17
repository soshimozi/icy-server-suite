using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public interface IToken
    {
        void Get(IInputBuffer buffer);
        void Print(IOutputBuffer buffer);

        bool IsDelimiter
        {
            get;
        }

        bool IsReservedWord
        {
            get;
        }

        Enums.TokenCode Code
        {
            get;
            set;
        }

        DataType Type
        {
            get;
            set;
        }

        DataValue TokenValue
        {
            get;
            set;
        }

        string TokenString
        {
            get;
            set;
        }
    }
}
