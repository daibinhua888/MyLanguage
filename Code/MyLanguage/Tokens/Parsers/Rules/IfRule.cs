using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Rules
{
    public class IfRule : IRule
    {
        private ASTParser validator;

        public IfRule(ASTParser validator)
        {
            this.validator = validator;
        }

        public void Validate()
        {
            IfStatementRule();
        }

        private void IfStatementRule()
        {
            this.validator.MatchAnd(TokenType.Identifier, "if");

            ConditionRule();//condition rule

            //block rule
            this.validator.RuleForward(StatementType.Block);

            //maybe exists else syntax
            //and maybe else block
            if (this.validator.CanRuleForward(StatementType.Else))
                this.validator.RuleForward(StatementType.Else);
            else
                this.validator.Match(TokenType.EndOfStatement);
        }

        private void ConditionRule()
        {
            this.validator.Match(TokenType.LeftRoundBracket);

            this.validator.Match(TokenType.Identifier);
            this.validator.Match(TokenType.CompareEquals);
            this.validator.Match(TokenType.Number);

            this.validator.Match(TokenType.RightRoundBracket);
        }


        public ASTrees.AST AST()
        {
            this.validator.MatchAnd(TokenType.Identifier, "if");

            ASTrees.AST root = new ASTrees.AST( ASTrees.ASTTypes.IfStatement);

            var conditionNode=ConditionRule_AST();//condition rule
            root.AddChild(conditionNode);

            //block rule
            if (!this.validator.CanRuleForward(StatementType.Block))
                throw new InvalidOperationException();
            
            var conditionOKNode=this.validator.RuleForward_AST(StatementType.Block);
            root.AddChild(conditionOKNode);

            //maybe exists else syntax
            //and maybe else block
            if (this.validator.CanRuleForward(StatementType.Else))
            {
                var elseNode=this.validator.RuleForward_AST(StatementType.Else);
                root.AddChild(elseNode);
            }
            else
            {
                if (this.validator.currentToken.Type == TokenType.EndOfStatement)
                    this.validator.Match(TokenType.EndOfStatement);
            }

            return root;
        }

        private ASTrees.AST ConditionRule_AST()
        {
            this.validator.Match(TokenType.LeftRoundBracket);

            ASTrees.AST root = new ASTrees.AST(ASTrees.ASTTypes.ConditionBranch);

            ASTrees.AST leftNode = new ASTrees.AST(this.validator.currentToken, ASTrees.ASTTypes.Variable);
            this.validator.Match(TokenType.Identifier);

            ASTrees.AST operatorNode = new ASTrees.AST(this.validator.currentToken, ASTrees.ASTTypes.Operator);
            this.validator.Match(TokenType.CompareEquals);

            ASTrees.AST numberNode = new ASTrees.AST(this.validator.currentToken, ASTrees.ASTTypes.NumberLiteral);
            this.validator.Match(TokenType.Number);

            this.validator.Match(TokenType.RightRoundBracket);

            root.AddChild(operatorNode);
            operatorNode.AddChild(leftNode);
            operatorNode.AddChild(numberNode);

            return root;
        }
    }
}
