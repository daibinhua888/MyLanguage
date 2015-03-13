
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.ASTParsers
{
    public class DefaultASTParser
    {
        private Queue<Token> tokens;
        public DefaultASTParser(Queue<Token> tokens)
        {
            this.tokens = tokens;
        }

        private Token currentToken;
        public Token Get()
        {
            this.currentToken=this.tokens.Dequeue();
            return this.currentToken;
        }

        public Token Peek(int peekPosition)
        {
            if (peekPosition < 0)
                throw new Exception();

            if (peekPosition > 1)
                throw new Exception();

            if (peekPosition == 0)
                return this.currentToken;

            return tokens.Peek();
        }
    }
}
