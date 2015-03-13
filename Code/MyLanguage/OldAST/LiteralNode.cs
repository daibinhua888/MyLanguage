using ConsoleApplication8.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.OldAST
{
    public class LiteralNode:BaseNode
    {
        public LiteralNode(Token token):base(token)
        { 
        }

        public override string ToString()
        {
            return string.Format("{0}", this._token.Text);
        }

        public override object Eval(VariableTables.Enviroment env)
        {
            switch(this._token.Type)
            {
                case TokenType.Number:
                    return int.Parse(this._token.Text);
                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }

            return null;
        }
    }
}
