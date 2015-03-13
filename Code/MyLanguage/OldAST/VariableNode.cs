using ConsoleApplication8.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.OldAST
{
    public class VariableNode:BaseNode
    {
        public VariableNode(Token token)
            : base(token)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", this._token.Text);
        }


        public override object Eval(VariableTables.Enviroment env)
        {
            throw new NotImplementedException();
        }

        public object Eval4Variable(VariableTables.Enviroment env, object result)
        { 
            env.Set(this._token.Text, result);

            return null;
        }
    }
}
