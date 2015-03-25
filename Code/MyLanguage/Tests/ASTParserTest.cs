using ConsoleApplication8.ASTrees;
using ConsoleApplication8.Tokens.Parsers;
using ConsoleApplication8.Tokens.Lexers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ConsoleApplication8.Utility;

namespace ConsoleApplication8.Tests
{
    public class ASTParserTest
    {
        public static void Test()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("=======   AST语法树测试     ========");

            Try.OnException = OnException;

            Try.Execute(() =>DoTest("a=100;"));
            Try.Execute(() =>DoTest("a=100+200;"));
            Try.Execute(() =>DoTest("a=100+200+a;"));
            Try.Execute(() =>DoTest("a=100+200+a-          23;"));

            Try.Execute(() =>DoTest("show a;"));
            Try.Execute(() =>DoTest("show a, b, 200;"));

            Try.Execute(() =>DoTest("{a=100+200+300;}"));
            Try.Execute(() =>DoTest("{show a, b, 200;}"));

            Try.Execute(() =>DoTest("if(a==100){a=100+b;}"));
            Try.Execute(() =>DoTest("if(a==100){a=100+b;}else {a=b+c;}", true));
            Try.Execute(() => DoTest("if(a==100){a=100+b-(10*20);}else {a=b+c;}", true));

            Try.Execute(() =>DoTest("while(a==100){a=100+b;}"));

            Try.Execute(() =>DoTest("a=100+200*2+300;"));

            Try.Execute(() => DoTest("a=100;"));
            Try.Execute(() => DoTest("a=100+30;"));
            Try.Execute(() => DoTest("a=100+300-200;"));
            Try.Execute(() => DoTest("a=(100+300)-200;"));
            Try.Execute(() => DoTest("a=100+(300-200);"));
            Try.Execute(() => DoTest("a=100+(300-200)+a;", true));
            Try.Execute(() => DoTest("a=100+(200*2)+300+(2*1);", true));
        }

        private static void OnException()
        {
            Console.WriteLine("         Exception");
        }

        private static void DoTest(string codes, bool specialDisplay = false)
        {
            var tokenParser = new Lexer(codes);

            var astParser = new ASTParser(tokenParser);

            AST root = astParser.ToAST();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("语句：{0}", codes);

            tmpCodes = codes;

            if (!specialDisplay)
                DisplayASTree(root);
            else
                DisplayASTByDot(root);
        }
        private static void DisplayASTree(AST tree)
        {
            Console.WriteLine("     "+tree.ToString());
            foreach (var node in tree.Children)
                DisplayASTree(node);
        }

        private static string dotContent = string.Empty;
        private static string nodeDefination = string.Empty;
        private static string tmpCodes = string.Empty;

        private static void DisplayASTByDot(AST root)
        {
            dotContent = string.Empty;
            nodeDefination = string.Empty;

            /*
            digraph hello {
                    A -> B;    A -> C;   B -> D;    C -> D
            }
            */
            WalkTree(root);

            string txtContext = @"
digraph hello {
                    Title[label=""" + tmpCodes + @"""]
                    " + nodeDefination + @"
                    " +dotContent+@"
            }
";

            string fileName = Guid.NewGuid().ToString("N");
            //save
            FileHelper.Save(txtContext, string.Format("{0}.txt", fileName));

            //execute
            var p = Process.Start("dot.exe", string.Format(" -Tpng {0}.txt -o {0}.png", fileName));
            p.WaitForExit();

            //display image file
            Process.Start(string.Format("{0}.png", fileName));
        }

        private static void WalkTree(AST root)
        {
            var hId=root.GetHashCode();

            nodeDefination += string.Format("{0}[label=\"{1}\"];", hId, root.ToString());

            foreach(var node in root.Children)
                dotContent += string.Format("{0} -> {1};", hId, node.GetHashCode());

            foreach (var node in root.Children)
                WalkTree(node);
        }
    }
}
