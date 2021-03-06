﻿using ConsoleApplication8.Tokens.Lexers;
using ConsoleApplication8.Tokens.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tests
{
    public class SyntaxValidatorTest
    {
        public static void Test()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("=======   语法测试     ========");

            Try.OnException = OnException;

            Try.Execute(() => DoTest("a=100+200;"));
            Try.Execute(() => DoTest("a=100;"));
            Try.Execute(() => DoTest("a=100   ;"));
            Try.Execute(() => DoTest("a=100  + 200 -   56;"));
            Try.Execute(() => DoTest("a=100  + 200 -   56  asdfa;"));
            Try.Execute(() => DoTest("myfunction a;"));
            Try.Execute(() => DoTest("myfunction 100;"));
            Try.Execute(() => DoTest("myfunction a, 100;"));
            Try.Execute(() => DoTest("myfunction a, 100, b;"));
            Try.Execute(() => DoTest("myfunction a, 100, b,              300;"));
            Try.Execute(() => DoTest("myfunction a, 100, b);"));
            Try.Execute(() => DoTest(@"{    a=100;}  "));
            Try.Execute(() => DoTest(@"{    myfunction a, 100, b;   }  "));
            Try.Execute(() => DoTest("{    myfuncti1on a, 100, b;   }  "));
            Try.Execute(() => DoTest("if(a==100){myfuncation 100;}"));
            Try.Execute(() => DoTest("if(a==100){myfuncation 100;}else{myfun111cation 200;}"));
            Try.Execute(() => DoTest("if(a==100){myfuncation 100;}else{myfun111cation 200, 564456546;}"));
            Try.Execute(() => DoTest("if(a==100){myfuncation 100;}else{myfun111cation 200, id3;}"));
            Try.Execute(() => DoTest("if(a==100){myfuncation 100;}else{myfun111cation 200 564456546;}"));
            Try.Execute(() => DoTest("if(a==100){myfuncation 100;}else{myfun111cation 200 ==564456546;}"));
            Try.Execute(() => DoTest("while(a==100){myfuncation 100;}"));
            Try.Execute(() => DoTest("while(a==100){a=100;}"));
            Try.Execute(() => DoTest("while(a==100){a=100+200;}"));
            Try.Execute(() => DoTest("while(a==100){a=100+200+600;}"));
            Try.Execute(() => DoTest("while(a==100){a=100+200+600+;}"));
            Try.Execute(() => DoTest("while(a==100){a=100+200+600+a;}"));
            Try.Execute(() => DoTest("while(a==100){a=a+1;}"));

            Try.Execute(() => DoTest("a=a+1;"));
            Try.Execute(() => DoTest("a=100+200*2+300;"));
            Try.Execute(() => DoTest("a=100+200*(2+300);"));
            Try.Execute(() => DoTest("a=100+200*(2+300*(1+2));"));
            Try.Execute(() => DoTest("a=100+200*(2+300*((1+2));"));
        }

        private static void OnException()
        {
            Console.WriteLine("         Exception");
        }

        private static void DoTest(string codes)
        {
            Console.WriteLine("testing - {0}", codes);

            var tokenParser = new Lexer(codes);

            var validator = new ASTParser(tokenParser);

            validator.Validate();

            Console.WriteLine("         Success");
        }
    }
}
