using ConsoleApplication8.Tokens.Lexers;
using ConsoleApplication8.Tokens.Parsers.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tests.SingleTests
{
    public class ASTreeValidator
    {
        public static void Test()
        {
            Console.WriteLine("*************Assign Statement Validate*******************");
            DoTest("a=100+200;");
            DoTest("a=100;");
            DoTest("a=100   ;");
            DoTest("a=100  + 200 -   56;");

            Utility.TryDo(() =>
            {
                DoTest("a=100  + 200 -   56  asdfa;");
            }, (ex) => { Console.WriteLine(ex.Message); });
            Console.WriteLine("*************Function Invoke Validate DONE*******************");


            Console.WriteLine("*************Function Invoke Validate*******************");
            DoTest("myfunction a;");
            DoTest("myfunction 100;");
            DoTest("myfunction a, 100;");
            DoTest("myfunction a, 100, b;");
            DoTest("myfunction a, 100, b,              300;");
            Utility.TryDo(() =>
            {
                DoTest("myfunction a, 100, b);");
            }, (ex) => { Console.WriteLine(ex.Message); });
            Console.WriteLine("*************Function Invoke Validate DONE*******************");


            Console.WriteLine("*************花括号验证 Validate*******************");
            DoTest(@"{    a=100;}  ");
            DoTest(@"{    myfunction a, 100, b;   }  ");
            Utility.TryDo(() =>
            {
                DoTest("{    myfuncti1on a, 100, b;   }  ");
            }, (ex) => { Console.WriteLine(ex.Message); });
            Console.WriteLine("*************花括号验证 Validate DONE*******************");
        }

        private static void DoTest(string codes)
        {
            var tokenParser = new Lexer(codes);

            var validator = new DefaultValidator(tokenParser);

            validator.Validate();
        }
    }
}
