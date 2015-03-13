using ConsoleApplication8.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tests.SingleTests
{
    class TokenTest
    {
        public static void Test()
        {
            //a=100+200;
            //show a;

            DisplayTokens(LoadLine1());

            DisplayTokens(LoadLine2());
        }

        private static void DisplayTokens(Queue<Token> queue)
        {
            Console.WriteLine("-------------------");

            while (queue.Count > 0)
            {
                var token = queue.Dequeue();
                Console.Write("{0}  ", token.Text, token.Type);
            }

            Console.WriteLine();
        }

        static Queue<Token> LoadLine1()
        {
            Queue<Token> tokens = new Queue<Token>();

            tokens.Enqueue(new Token("a", TokenType.Identifier));
            tokens.Enqueue(new Token(TokenType.Equals));
            tokens.Enqueue(new Token("100", TokenType.Number));
            tokens.Enqueue(new Token(TokenType.Plus));
            tokens.Enqueue(new Token("200", TokenType.Number));
            tokens.Enqueue(new Token("EOS", TokenType.EndOfStatement));

            return tokens;
        }

        static Queue<Token> LoadLine2()
        {
            Queue<Token> tokens = new Queue<Token>();

            tokens = new Queue<Token>();
            tokens.Enqueue(new Token("show", TokenType.Identifier));
            tokens.Enqueue(new Token("a", TokenType.Identifier));
            tokens.Enqueue(new Token("", TokenType.EndOfStatement));

            return tokens;
        }
    }
}
