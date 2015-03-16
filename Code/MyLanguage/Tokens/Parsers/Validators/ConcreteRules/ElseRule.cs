using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Validators.ConcreteRules
{
    public class ElseRule : IRuleProcessor
    {
        private SyntaxValidator validator;

        public ElseRule(SyntaxValidator validator)
        {
            this.validator = validator;
        }

        public void ProcessRule()
        {
            ElseStatementRule();
        }

        private void ElseStatementRule()
        {
            this.validator.MatchAnd(TokenType.Identifier, "else");

            this.validator.RuleForward(StatementType.Block);
        }
    }
}
