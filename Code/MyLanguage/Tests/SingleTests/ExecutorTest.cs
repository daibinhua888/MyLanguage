using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tests.SingleTests
{
    class ExecutorTest
    {
        internal static void Test()
        {
            var env = new VariableTables.Enviroment();

            var ast1=ASTTest.ParseLine1();

            var result1=ast1.Eval(env);

            var ast2 = ASTTest.ParseLine2();

            var result2 = ast2.Eval(env);
        }
    }
}
