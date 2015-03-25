using ConsoleApplication8.ASTrees;
using ConsoleApplication8.RuntimeEnviroment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Interpreters
{
    public class DefaultInterpreter
    {
        private SymbolTable enviroment;
        private ASTProcessor astProcessor;

        public DefaultInterpreter()
        {
            this.enviroment = new SymbolTable();
            this.astProcessor = new ASTProcessor();
        }

        public SymbolTable Environment { get { return this.enviroment; } }

        public void Execute(AST ast)
        {
            astProcessor.Handle(ast, this.enviroment);
        }
    }
}
