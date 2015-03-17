using ConsoleApplication8.ASTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Rules
{
    public class AssignRule:IRule
    {
        private ASTParser validator;

        public AssignRule(ASTParser validator)
        {
            this.validator = validator;
        }

        public void Validate()
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
            else
                throw new InvalidOperationException();
        }

        //NUMBER (+|-|*|/) NUMBER (+|-|*|/) NUMBER (+|-|*|/) NUMBER
        private void ExpressionRule_MultipleNumberExpression()
        {
            this.validator.MatchOne(TokenType.Number, TokenType.Identifier);
            while (this.validator.currentToken.Type != TokenType.EndOfStatement)
            {
                this.validator.MatchOne(TokenType.Plus, TokenType.Minus, TokenType.Multiply, TokenType.Divide);
                this.validator.MatchOne(TokenType.Number, TokenType.Identifier);
            }
            this.validator.Match(TokenType.EndOfStatement);
        }


        public ASTrees.AST AST()
        {
            AST root = new AST( ASTTypes.AssignStatement);

            AST leftVariableNode = new AST(this.validator.currentToken, ASTTypes.Variable);

            root.AddChild(leftVariableNode);

            this.validator.Consume();       //consume variable token
            this.validator.Consume();       //consume = token

            AST rightExpressionNode=ExpressionAST();

            root.AddChild(rightExpressionNode);

            return root;
        }

        private AST ExpressionAST()
        {
            if (this.validator.CanRuleForward(ExpressionRule_MultipleNumberExpression))
                return ExpressionRule_MultipleNumberExpression_AST();
            else
                throw new InvalidOperationException();
        }

        private AST ExpressionRule_MultipleNumberExpression_AST()
        {
            AST rootNode = new ASTrees.AST(ASTTypes.Expression);

            AST leftNode = GetNumberOrVariableNode();
            rootNode.AddChild(leftNode);

            var subRightNode = leftNode;
            var parentNode = rootNode;

            while (this.validator.currentToken.Type != TokenType.EndOfStatement)
            {
                var operatorNode = new AST(this.validator.currentToken, ASTTypes.Operator);
                operatorNode.AddChild(subRightNode);

                this.validator.Consume();

                AST rightNode = GetNumberOrVariableNode();

                operatorNode.AddChild(rightNode);

                parentNode.RemoveChild(subRightNode);
                parentNode.AddChild(operatorNode);

                subRightNode = rightNode;
                parentNode = operatorNode;
            }

            this.validator.Consume();       //end of statement

            return rootNode;
        }

        private AST GetNumberOrVariableNode()
        {
            AST node = null;
            if (this.validator.currentToken.Type == TokenType.Number)
            {
                node = new AST(this.validator.currentToken, ASTTypes.NumberLiteral);
                this.validator.Consume();
            }
            else if (this.validator.currentToken.Type == TokenType.Identifier)
            {
                node = new AST(this.validator.currentToken, ASTTypes.Variable);
                this.validator.Consume();
            }
            else
            {
                throw new Exception();
            }
            return node;
        }
    }
}
