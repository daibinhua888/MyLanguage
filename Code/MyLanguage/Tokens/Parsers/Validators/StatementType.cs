using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Validators
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
