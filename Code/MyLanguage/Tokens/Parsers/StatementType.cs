using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers
{
    public enum StatementType
    {
        Invalid,
        Assign,
        FunctionInvoke,
        Block,
        If,
        Else,
        While
    }
}
