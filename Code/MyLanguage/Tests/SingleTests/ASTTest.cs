using ConsoleApplication8.OldAST;
using ConsoleApplication8.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tests.SingleTests
{
    class ASTTest
    {
        public static void Test()
        {
            Console.WriteLine("-------------------");
            BaseNode line1AST = ParseLine1();
            Console.WriteLine(line1AST.ToString());
            Console.WriteLine();

            Console.WriteLine("-------------------");
            BaseNode line2AST = ParseLine2();
            Console.WriteLine(line2AST.ToString());
            Console.WriteLine();
        }

        public static BaseNode ParseLine1()
        {
            AssignNode assignNode = new AssignNode(new Token(TokenType.Equals));
            assignNode.LeftNode = new OldAST.VariableNode(new Token("a", TokenType.Identifier));

            var binaryNode = new BinaryOperatorNode(new Token(TokenType.Plus));
            binaryNode.LeftNode = new LiteralNode(new Token("100", TokenType.Number));
            binaryNode.RightNode = new LiteralNode(new Token("200", TokenType.Number));

            assignNode.ExpressionNode = binaryNode;

            return assignNode;
        }

        public static BaseNode ParseLine2()
        {
            CallNode callNode = new CallNode(new Token("show", TokenType.Identifier));
            callNode.Variable1 = new VariableNode(new Token("a", TokenType.Identifier));
            return callNode;
        }
    }
}
