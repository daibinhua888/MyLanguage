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
            Rule();
        }

        private void Rule()
        {
            this.validator.Match(TokenType.Identifier);
            this.validator.Match(TokenType.Equals);
            Expression();
        }

        private void Expression()
        {
            while(this.validator.currentToken.Type != TokenType.EndOfStatement
                &&
                this.validator.currentToken.Type != TokenType.RightRoundBracket)
            {
                if (this.validator.currentToken.Type == TokenType.LeftRoundBracket)
                {
                    Term();
                }
                else if (this.validator.currentToken.Type == TokenType.Number
                    ||
                    this.validator.currentToken.Type == TokenType.Identifier)
                {
                    SingleNumber();
                }
                else
                {
                    throw new InvalidOperationException();
                }

                if (this.validator.currentToken.Type != TokenType.EndOfStatement
                    &&
                    this.validator.currentToken.Type != TokenType.RightRoundBracket)
                    this.validator.MatchOne(TokenType.Plus, TokenType.Minus, TokenType.Multiply, TokenType.Divide);
            }
        }

        private void Term()
        {
            this.validator.Match(TokenType.LeftRoundBracket);

            while (this.validator.currentToken.Type != TokenType.EndOfStatement
                &&
                this.validator.currentToken.Type != TokenType.RightRoundBracket)
            {
                Expression();
            }

            this.validator.Match(TokenType.RightRoundBracket);
        }
        
        private void SingleNumber()
        {
            this.validator.MatchOne(TokenType.Number, TokenType.Identifier);
        }


        public ASTrees.AST AST()
        {
            AST root = new AST( ASTTypes.AssignStatement);

            AST left = new AST(this.validator.currentToken, ASTTypes.Variable);

            root.AddChild(left);

            this.validator.Consume();       //consume variable token
            this.validator.Consume();       //consume = token

            AST right=Expression_AST();

            root.AddChild(right);

            return root;
        }

        private AST Expression_AST()
        {
            AST rootNode = new ASTrees.AST(ASTTypes.Expression);
            AST leftNode = GetOneNode();
            rootNode.AddChild(leftNode);

            var subRightNode = leftNode;
            var parentNode = rootNode;

            while (this.validator.currentToken.Type != TokenType.EndOfStatement
                &&
                this.validator.currentToken.Type != TokenType.RightRoundBracket)
            {
                var operatorNode = new AST(this.validator.currentToken, ASTTypes.Operator);
                operatorNode.AddChild(subRightNode);

                this.validator.Consume();
                AST rightNode = GetOneNode();

                operatorNode.AddChild(rightNode);

                parentNode.RemoveChild(subRightNode);
                parentNode.AddChild(operatorNode);

                subRightNode = rightNode;
                parentNode = operatorNode;
            }

            this.validator.Consume();       //end of statement

            return rootNode;
        }

        private ASTrees.AST GetOneNode()
        {
            if (this.validator.currentToken.Type == TokenType.LeftRoundBracket)
            {
                return Term_AST();
            }
            else if (this.validator.currentToken.Type == TokenType.Number
                ||
                this.validator.currentToken.Type == TokenType.Identifier)
            {
                return SingleNumber_AST();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private AST Term_AST()
        {
            AST rootNode = new ASTrees.AST(ASTTypes.Term);

            this.validator.Match(TokenType.LeftRoundBracket);

            //while (this.validator.currentToken.Type != TokenType.EndOfStatement
            //    &&
            //    this.validator.currentToken.Type != TokenType.RightRoundBracket)
            //{
            //    rootNode.AddChild(Expression_AST());
            //}
            rootNode.AddChild(Expression_AST());

            //this.validator.Match(TokenType.RightRoundBracket);

            return rootNode;
        }

        private AST SingleNumber_AST()
        {
            return RuleHelper.GetNumberOrVariableNode(this.validator);
        }
    }
}
