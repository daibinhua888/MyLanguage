using ConsoleApplication8.Tokens;
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

        public AST(ASTTypes astType):this(null, astType)
        { 
        }
        public AST(Token token, ASTTypes astType)
        {
            this._token = token;

            this._astType = astType;

            this._children = new List<AST>();
        }

        public override string ToString()
        {
            if (this.Token!=null)
                return string.Format("{0}({1}/{2})", this.ASTType, this.Token.Text, this.Token.Type);
            else
                return string.Format("{0}(null)", this.ASTType);
        }

        public void AddChild(AST node)
        {
            this._children.Add(node);
        }

        public void RemoveChild(AST currentNode)
        {
            this._children.Remove(currentNode);
        }
    }
}
