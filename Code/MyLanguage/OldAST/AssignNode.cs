using ConsoleApplication8.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.OldAST
{
    public class AssignNode:BaseNode
    {
        public AssignNode(Token token)
            : base(token)
        { 
        }

        public VariableNode LeftNode { get; set; }
        public BaseNode ExpressionNode { get; set; }


        public override string ToString()
        {
            return string.Format("{0}{1}{2}", LeftNode.ToString(), this._token.Text, ExpressionNode.ToString());
        }

        public override object Eval(VariableTables.Enviroment env)
        {
            var expressionNodeResult = ExpressionNode.Eval(env);

            LeftNode.Eval4Variable(env, expressionNodeResult);

            return null;
        }
    }
}
