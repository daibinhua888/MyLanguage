using ConsoleApplication8.Tokens.Lexers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tests
{
    class TokenParserTests
    {
        internal static void Test()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("=======   Token分词测试     ========");

            DoTest("a=100+200;");

            DoTest("a=100+200; ");
            DoTest("a   =  100+200; ");
            DoTest("show a;");
            DoTest("               show a  ; ");
            DoTest("               show            a;   ");
            DoTest(@"
                        a=100+200;
                        show            a   ;

");
            DoTest("a=\"testing\";");       //目前还不支持双引号之中套双引号
            DoTest(@"a=""testing"";");      //同上

            DoTest("show a;");
            DoTest("show a, 100;");

            DoTest(@"
a=100  +   200         ;
{
    b=a+1;
    c=100;
}
");

            DoTest("show a, 100;");

            DoTest("if(a==100){myfuncation 100;}else{myfun111cation 200;}");

            DoTest("a=100+200*2+300; ");
        }

        private static void DoTest(string codes)
        {
            var parser = new Lexer(codes);

            while (true)
            {
                var token = parser.GetNextToken();

                if (token == null)
                    break;

                Console.Write("<{0}, {1}>, ", token.Type, token.Text);
            }

            Console.WriteLine();
        }
    }
}
