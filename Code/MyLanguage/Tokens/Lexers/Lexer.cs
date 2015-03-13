using ConsoleApplication8.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Lexers
{
    public class Lexer
    {
        private string rawString;
        private LexerProcessor parser;

        public Lexer(string rawString)
        {
            this.rawString = rawString;
            parser = new LexerProcessor(this.rawString);
        }

        public Token GetNextToken()
        {
            var token = parser.RecognizeToken();

            if (token.Type == TokenType.Invalid)
                return null;

            return token;
        }

        public List<Token> GetAllTokens()
        {
            List<Token> tokens = new List<Token>();

            while (true)
            {
                var token = GetNextToken();

                if (token == null)
                    break;

                tokens.Add(token);
            }

            return tokens;
        }
    }
}
