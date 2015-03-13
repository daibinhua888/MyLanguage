
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.ASTParsers
{
    public class DefaultASTParser
    {
        private List<Token> tokens;
        public DefaultASTParser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        private int position = -1;

        public int GetPosition()
        {
            return this.position;
        }

        public void SetPosition(int position)
        {
            this.position = position;
        }

        public Token Get()
        {
            this.position++;
            return this.tokens[this.position];
        }

        public Token Peek(int peekPosition)
        {
            if (this.position + peekPosition >= this.tokens.Count)
                throw new InvalidOperationException();

            return this.tokens[this.position + peekPosition];
        }
    }
}
