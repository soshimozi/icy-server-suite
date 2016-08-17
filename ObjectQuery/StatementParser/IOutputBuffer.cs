using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public interface IOutputBuffer
    {
        void PutLine();
        void PutLine(string line);
    }
}
