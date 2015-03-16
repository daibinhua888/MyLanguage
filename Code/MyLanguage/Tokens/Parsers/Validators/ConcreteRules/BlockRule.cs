using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Validators.ConcreteRules
{
    public class BlockRule : IRuleProcessor
    {
        private SyntaxValidator validator;

        public BlockRule(SyntaxValidator validator)
        {
            this.validator = validator;
        }

        public void ProcessRule()
        {
            BlockStatementRule();
        }

        private void BlockStatementRule()
        {
            this.validator.Match(TokenType.LeftBrace);

            if (this.validator.CanRuleForward(StatementType.Assign))
                this.validator.RuleForward(StatementType.Assign);
            else if (this.validator.CanRuleForward(StatementType.FunctionInvoke))
                this.validator.RuleForward(StatementType.FunctionInvoke);
            else
                throw new InvalidOperationException();

            this.validator.Match(TokenType.RightBrace);
        }
    }
}
