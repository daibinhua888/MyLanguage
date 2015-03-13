using ConsoleApplication8.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.OldAST
{
    public class BinaryOperatorNode:BaseNode
    {
        public BinaryOperatorNode(Token token)
            : base(token)
        { 
        }

        public BaseNode LeftNode { get; set; }
        public BaseNode RightNode { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", LeftNode.ToString(), this._token.Text, RightNode.ToString());
        }

        public override object Eval(VariableTables.Enviroment env)
        {
            switch(this._token.Type)
            {
                case  TokenType.Plus:
                    return int.Parse(LeftNode.Value_String) + int.Parse(RightNode.Value_String);
                    break;
                case TokenType.Minus:
                    return int.Parse(LeftNode.Value_String) - int.Parse(RightNode.Value_String);
                    break;
                case TokenType.Multiply:
                    return int.Parse(LeftNode.Value_String) * int.Parse(RightNode.Value_String);
                    break;
                case TokenType.Divide:
                    return int.Parse(LeftNode.Value_String) / int.Parse(RightNode.Value_String);
                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }
            return null;
        }
    }
}
