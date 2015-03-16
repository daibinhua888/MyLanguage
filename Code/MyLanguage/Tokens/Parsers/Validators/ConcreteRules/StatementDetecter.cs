using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Validators.ConcreteRules
{
    public class StatementDetecter
    {
        private SyntaxValidator validator;

        public StatementDetecter(SyntaxValidator validator)
        {
            this.validator = validator;
        }

        public StatementType DetectStatementType()
        {
            if (validator.currentToken.Type == TokenType.Identifier && validator.PeekToken(1).Type == TokenType.Equals)
                return StatementType.Assign;
            else if (validator.currentToken.Type == TokenType.Identifier && validator.currentToken.Text == "if")
                return StatementType.If;
            else if (validator.currentToken.Type == TokenType.Identifier && validator.currentToken.Text == "while")
                return StatementType.While;
            else if (validator.currentToken.Type == TokenType.Identifier && validator.PeekToken(1).Type != TokenType.Equals)
                return StatementType.FunctionInvoke;
            else if (validator.currentToken.Type == TokenType.LeftBrace)
                return StatementType.Block;
            else
                return StatementType.Invalid;
        }
    }
}
