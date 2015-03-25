using ConsoleApplication8.ASTrees;
using ConsoleApplication8.Tokens.Interpreters;
using ConsoleApplication8.Tokens.Lexers;
using ConsoleApplication8.Tokens.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tests
{
    class InterpreterTest
    {
        public static void Test()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("=======   解释器测试     ========");

            Try.OnException = OnException;

            DoTest("a=100+200;");

            DoTest("a=100+200-600;");
        }

        private static void DoTest(string codes)
        {
            var tokenParser = new Lexer(codes);

            var astParser = new ASTParser(tokenParser);

            AST root = astParser.ToAST();

            DefaultInterpreter interpreter = new DefaultInterpreter();
            interpreter.Execute(root);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(codes);

            DisplayEnviroment(interpreter.Environment);
        }

        private static void DisplayEnviroment(RuntimeEnviroment.SymbolTable symbolTable)
        {
            if (symbolTable.AllKeys.Count == 0)
                Console.WriteLine("          Empty");

            foreach(var e in symbolTable.AllKeys)
                Console.WriteLine("         {0}: {1}", e, symbolTable.Get(e).ToString());
        }

        private static void OnException()
        {
            Console.WriteLine("         Exception");
        }
    }
}

