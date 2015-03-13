using ConsoleApplication8.Tokens.ASTValidators;
using ConsoleApplication8.Tokens.Lexers;
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
            DoTest("a=100+200;");

//            DoTest(@"
//
//show a;
//
//");
        }

        private static void DoTest(string codes)
        {
            var tokenParser = new Lexer(codes);

            var validator = new DefaultASTValidatorWrapper(tokenParser);

            validator.Validate();
        }
    }
}
