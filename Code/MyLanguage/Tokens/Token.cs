using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens
{
    public class Token
    {
        private string _text;
        private TokenType _type;

        public Token(string text, TokenType type)
        {
            this._text = text;
            this._type = type;
        }

        public Token(TokenType type):this(string.Empty, type)
        {
        }

        public string Text
        {
            get
            {
                return this._text;
            }
        }

        public TokenType Type
        {
            get
            {
                return this._type;
            }
        }
    }
}
