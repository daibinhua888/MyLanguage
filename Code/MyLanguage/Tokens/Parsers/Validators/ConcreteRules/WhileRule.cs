using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Validators.ConcreteRules
{
    public class WhileRule : IRuleProcessor
    {
        private SyntaxValidator validator;

        public WhileRule(SyntaxValidator validator)
        {
            this.validator = validator;
        }

        public void ProcessRule()
        {
            WhileStatementRule();
        }

        private void WhileStatementRule()
        {
            this.validator.MatchAnd(TokenType.Identifier, "while");

            ConditionRule();//condition rule

            //block rule
            this.validator.RuleForward(StatementType.Block);
        }

        private void ConditionRule()
        {
            this.validator.Match(TokenType.LeftRoundBracket);

            this.validator.Match(TokenType.Identifier);
            this.validator.Match(TokenType.CompareEquals);
            this.validator.Match(TokenType.Number);

            this.validator.Match(TokenType.RightRoundBracket);
        }
    }
}
