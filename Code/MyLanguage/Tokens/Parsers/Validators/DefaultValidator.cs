using ConsoleApplication8.ASTrees;
using ConsoleApplication8.Tokens.Parsers;
using ConsoleApplication8.Tokens.Lexers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication8.Tokens.Parsers.ASTParsers;

namespace ConsoleApplication8.Tokens.Parsers.Validators
{
    public class DefaultValidator:BaseValidator
    {
        public DefaultValidator(Lexer tokenParser):base(tokenParser)
        {
        }

        public override void Validate()
        {
            ReadToken();

            if (currentToken.Type == TokenType.Identifier && PeekToken(1).Type == TokenType.Equals)
                AssignStatementRule();
            else if (currentToken.Type == TokenType.Identifier && PeekToken(1).Type != TokenType.Equals)
                FunctionInvokeStatementRule();
            else if(currentToken.Type== TokenType.LeftBrace)
                BlockStatementRule();
            else
                throw new InvalidOperationException("不支持的语句");
        }

        private void BlockStatementRule()
        {
            Match(TokenType.LeftBrace);

            if (CanRuleForward(AssignStatementRule))
                AssignStatementRule();
            else if (CanRuleForward(FunctionInvokeStatementRule))
                FunctionInvokeStatementRule();
            else
                throw new InvalidOperationException();

            Match(TokenType.RightBrace);
        }

        private void FunctionInvokeStatementRule()
        {
            Match(TokenType.Identifier);

            MatchOne(TokenType.Identifier, TokenType.Number);

            while (this.currentToken.Type != TokenType.EndOfStatement)
            {
                Match(TokenType.Comma);
                MatchOne(TokenType.Identifier, TokenType.Number);
            }

            Match(TokenType.EndOfStatement);
        }

        private void AssignStatementRule()
        {
            Match(TokenType.Identifier);
            Match(TokenType.Equals);
            ExpressionRule();
        }

        private void ExpressionRule()
        {
            if (CanRuleForward(ExpressionRule_MultipleNumberExpression))
                ExpressionRule_MultipleNumberExpression();
            else if (CanRuleForward(ExpressionRule_SingleNumberExpression))
                ExpressionRule_SingleNumberExpression();
            else
                throw new InvalidOperationException();
        }

        //NUMBER
        private void ExpressionRule_SingleNumberExpression()
        {
            Match(TokenType.Number);
            Match(TokenType.EndOfStatement);
        }

        //NUMBER (+|-|*|/) NUMBER (+|-|*|/) NUMBER (+|-|*|/) NUMBER
        private void ExpressionRule_MultipleNumberExpression()
        {
            Match(TokenType.Number);
            while (this.currentToken.Type!= TokenType.EndOfStatement)
            {
                MatchOne(TokenType.Plus, TokenType.Minus, TokenType.Multiply, TokenType.Divide);
                Match(TokenType.Number);
            }
            Match(TokenType.EndOfStatement);
        }
    }
}
