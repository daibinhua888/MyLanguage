using ConsoleApplication8.Tests;
using ConsoleApplication8.Tests.SingleTests;
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
            TokenParserTests.Test();

            TokenTest.Test();

            ASTTest.Test();

            ExecutorTest.Test();

            ASTreeValidator.Test();

            //ASTreeTest.Test();

            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
