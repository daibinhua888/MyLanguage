using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Validators.ConcreteRules
{
    public class AssignRule:IRuleProcessor
    {
        private SyntaxValidator validator;

        public AssignRule(SyntaxValidator validator)
        {
            this.validator = validator;
        }

        public void ProcessRule()
        {
            AssignStatementRule();
        }

        private void AssignStatementRule()
        {
            this.validator.Match(TokenType.Identifier);
            this.validator.Match(TokenType.Equals);
            ExpressionRule();
        }

        private void ExpressionRule()
        {
            if (this.validator.CanRuleForward(ExpressionRule_MultipleNumberExpression))
                ExpressionRule_MultipleNumberExpression();
            else if (this.validator.CanRuleForward(ExpressionRule_SingleNumberExpression))
                ExpressionRule_SingleNumberExpression();
            else
                throw new InvalidOperationException();
        }

        //NUMBER
        private void ExpressionRule_SingleNumberExpression()
        {
            this.validator.Match(TokenType.Number);
            this.validator.Match(TokenType.EndOfStatement);
        }

        //NUMBER (+|-|*|/) NUMBER (+|-|*|/) NUMBER (+|-|*|/) NUMBER
        private void ExpressionRule_MultipleNumberExpression()
        {
            this.validator.Match(TokenType.Number);
            while (this.validator.currentToken.Type != TokenType.EndOfStatement)
            {
                this.validator.MatchOne(TokenType.Plus, TokenType.Minus, TokenType.Multiply, TokenType.Divide);
                this.validator.Match(TokenType.Number);
            }
            this.validator.Match(TokenType.EndOfStatement);
        }
    }
}
