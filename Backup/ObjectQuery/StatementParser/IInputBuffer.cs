using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public interface IInputBuffer
    {
        int ErrorCount
        {
            get;
            set;
        }

        int CurrentPosition
        {
            get;
        }

        char CurrentChar
        {
            get;
        }

        char GetChar();
        char PutBackChar();

        void GetLine();
        int LineNumber
        {
            get;
        }

        bool ListSource
        {
            get;
            set;
        }
    }
}
