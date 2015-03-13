using ConsoleApplication8.UserFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.OldAST
{
    public class CallNode:BaseNode
    {
        public CallNode(Tokens.Token token):base(token)
        { 
        }

        public override string ToString()
        {
            if(Variable1!=null)
                return string.Format("{0}({1})", this._token.Text, Variable1.ToString());

            return string.Format("{0}()", this._token.Text);
        }

        public VariableNode Variable1 { get; set; }


        public override object Eval(VariableTables.Enviroment env)
        {
            if (this._token.Text == "show")
            {
                List<int> lst = new List<int>();

                if (this.Variable1 != null)
                {
                    var v1Value = env.Get(this.Variable1.Value_String);

                    lst.Add(int.Parse(v1Value.ToString()));
                }

                _01.Show(lst.ToArray());
            }

            return null;
        }
    }
}
