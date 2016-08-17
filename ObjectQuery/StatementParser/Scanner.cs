using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public interface IScanner
    {
        IToken Get();

        IToken EOFToken
        {
            get;
        }

        IToken ErrorToken
        {
            get;
        }
    }
}
