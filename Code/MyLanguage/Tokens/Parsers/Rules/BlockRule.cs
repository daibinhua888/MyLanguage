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
            ASTrees.AST root = new ASTrees.AST(ASTrees.ASTTypes.BlockStatement);

            this.validator.Match(TokenType.LeftBrace);

            ASTrees.AST subNode = null;
            if (this.validator.CanRuleForward(StatementType.Assign))
                subNode=this.validator.RuleForward_AST(StatementType.Assign);
            else if (this.validator.CanRuleForward(StatementType.FunctionInvoke))
                subNode = this.validator.RuleForward_AST(StatementType.FunctionInvoke);
            else
                throw new InvalidOperationException();

            while(this.validator.currentToken.Type== TokenType.EndOfStatement)
                this.validator.Consume();

            this.validator.Match(TokenType.RightBrace);

            root.AddChild(subNode);

            return root;
        }
    }
}
