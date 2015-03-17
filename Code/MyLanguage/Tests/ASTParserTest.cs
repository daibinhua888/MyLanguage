using ConsoleApplication8.ASTrees;
using ConsoleApplication8.Tokens.Parsers;
using ConsoleApplication8.Tokens.Lexers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tests
{
    public class ASTParserTest
    {
        public static void Test()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("=======   AST语法树测试     ========");

            DoTest("a=100;");
            DoTest("a=100+200;");
            DoTest("a=100+200+a;");
            DoTest("a=100+200+a-          23;");

//            DoTest(@"
//
//show a;
//
//");
        }

        private static void DoTest(string codes)
        {
            var tokenParser = new Lexer(codes);

            var astParser = new ASTParser(tokenParser);

            AST root = astParser.ToAST();

            Console.WriteLine();
            Console.WriteLine();

            DisplayASTree(root);
        }

        //需要换成dot来展示树形结构
        private static void DisplayASTree(AST tree)
        {
            Console.WriteLine(tree.ToString());
            foreach (var node in tree.Children)
                DisplayASTree(node);
        }
    }
}
