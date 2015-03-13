using ConsoleApplication8.Tokens;
using ConsoleApplication8.VariableTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.OldAST
{
    public abstract class BaseNode
    {
        protected Token _token;

        public BaseNode(Token token)
        {
            this._token = token;
        }

        public string Value_String { get { return this._token.Text; } }

        public int Value_Int { get { return int.Parse(this._token.Text); } }

        public abstract override string ToString();

        public abstract object Eval(Enviroment env);
    }
}
