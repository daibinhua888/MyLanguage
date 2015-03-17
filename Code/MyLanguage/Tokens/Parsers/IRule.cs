using ConsoleApplication8.ASTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers
{
    public interface IRule
    {
        void Validate();
        AST AST();
    }
}
