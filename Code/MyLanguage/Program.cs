using ConsoleApplication8.Tests;
using ConsoleApplication8.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    class Program
    {
        static void Main(string[] args)
        {
            TokenParserTests.Test();                //分词测试

            SyntaxValidatorTest.Test();             //语法测试

            ASTParserTest.Test();                   //构造AST树测试

            InterpreterTest.Test();                 //解释器测试

            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
