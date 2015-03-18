using ConsoleApplication8.ASTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Rules
{
    class RuleHelper
    {
        public static AST GetNumberOrVariableNode(ASTParser validator)
        {
            AST node = null;
            if (validator.currentToken.Type == TokenType.Number)
            {
                node = new AST(validator.currentToken, ASTTypes.NumberLiteral);
                validator.Consume();
            }
            else if (validator.currentToken.Type == TokenType.Identifier)
            {
                node = new AST(validator.currentToken, ASTTypes.Variable);
                validator.Consume();
            }
            else
            {
                throw new Exception();
            }
            return node;
        }
    }
}
