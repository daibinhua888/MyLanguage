using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Rules
{
    public class BlockRule : IRule
    {
        private ASTParser validator;

        public BlockRule(ASTParser validator)
        {
            this.validator = validator;
        }

        public void Validate()
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


        public ASTrees.AST AST()
        {
            throw new NotImplementedException();
        }
    }
}
