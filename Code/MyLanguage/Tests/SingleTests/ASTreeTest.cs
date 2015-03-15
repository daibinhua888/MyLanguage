using ConsoleApplication8.ASTrees;
using ConsoleApplication8.Tokens.Parsers;
using ConsoleApplication8.Tokens.Lexers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication8.Tokens.Parsers.ASTParsers;

namespace ConsoleApplication8.Tests.SingleTests
{
    public class ASTreeTest
    {
        public static void Test()
        {
            DoTest("a=100+200;");

            DoTest(@"

show a;

");
        }

        private static void DoTest(string codes)
        {
            var tokenParser = new Lexer(codes);

            var astParser = new DefaultASTParserWrapper(tokenParser);

            AST tree = astParser.ConstructAST();

            Console.WriteLine();
            Console.WriteLine();
            DisplayASTree(tree);
        }

        private static void DisplayASTree(AST tree)
        {
            Console.WriteLine(tree.ToString());
            foreach (var node in tree.Children)
                DisplayASTree(node);
        }
    }
}
