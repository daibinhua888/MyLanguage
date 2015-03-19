using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Rules
{
    public class ElseRule : IRule
    {
        private ASTParser validator;

        public ElseRule(ASTParser validator)
        {
            this.validator = validator;
        }

        public void Validate()
        {
            ElseStatementRule();
        }

        private void ElseStatementRule()
        {
            this.validator.MatchAnd(TokenType.Identifier, "else");

            this.validator.RuleForward(StatementType.Block);
        }


        public ASTrees.AST AST()
        {
            this.validator.MatchAnd(TokenType.Identifier, "else");

            ASTrees.AST root = new ASTrees.AST(ASTrees.ASTTypes.ElseBranch);

            ASTrees.AST subNode = this.validator.RuleForward_AST(StatementType.Block);

            root.AddChild(subNode);

            return root;
        }
    }
}
