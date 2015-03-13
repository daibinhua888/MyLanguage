using ConsoleApplication8.Tokens;
using ConsoleApplication8.VariableTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.ASTrees
{
    public class AST
    {
        private List<AST> _children;
        private ASTTypes _astType;
        private Token _token;

        public Token Token { get { return this._token; } }
        public List<AST> Children { get { return this._children.AsReadOnly().ToList(); } }
        public ASTTypes ASTType { get { return this._astType; } }

        public AST(Token token, ASTTypes astType)
        {
            this._token = token;
            this._astType = astType;
            this._children = new List<AST>();
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", this.ASTType, this.Token.Text);
        }

        public void AddChild(AST node)
        {
            this._children.Add(node);
        }

        //public object Eval(Enviroment env)
        //{
        //    if (this._astType == ASTTypes.AssignStatement)
        //    {
        //        object rightValue = this.Children[1].Eval(env);

        //        this.Children[0].Eval(env, rightValue);
        //    }
        //    else if(this._astType== ASTTypes.Expression)
        //    {
        //    }

        //    return null;
        //}

        //public object Eval(Enviroment env, object value)
        //{
        //    if (this._astType == ASTTypes.Variable)
        //    {
        //        env.Set(this.Token.Text, value);

        //        return value;
        //    }

        //    return null;
        //}
    }
}
