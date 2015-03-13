using ConsoleApplication8.ASTrees;
using ConsoleApplication8.Tokens.ASTParsers;
using ConsoleApplication8.Tokens.Lexers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.ASTValidators
{
    public class DefaultASTValidatorWrapper
    {
        private DefaultASTParser astParser;

        public DefaultASTValidatorWrapper(Lexer tokenParser)
        {
            this.astParser = new DefaultASTParser(tokenParser.GetAllTokens());
        }

        private Token currentToken;
        private int savePoint_Position;

        public void Validate()
        {
            ReadToken();

            if (currentToken.Type == TokenType.Identifier && PeekToken(1).Type == TokenType.Equals)
                AssignStatementRule();
            else if (currentToken.Type == TokenType.Identifier && PeekToken(1).Type != TokenType.Equals)
                FunctionInvokeStatementRule();
            else
                throw new InvalidOperationException("不支持的语句");
        }

        private Token PeekToken(int position)
        {
            return astParser.Peek(position);
        }

        private void ReadToken()
        {
            this.currentToken = astParser.Get();
        }

        #region 方法调用语句
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
        #endregion

        #region 赋值语句
        private void AssignStatementRule()
        {
            Match(TokenType.Identifier);
            Match(TokenType.Equals);
            ExpressionRule();
        }

        private void ExpressionRule()
        {
            if (ExpressionRule_IsMultipleNumberExpression())
                ExpressionRule_MultipleNumberExpression();
            else if (ExpressionRule_IsSingleNumberExpression())
                ExpressionRule_SingleNumberExpression();
            else
                throw new InvalidOperationException();
        }

        #region SingleNumberExpression
        private bool ExpressionRule_IsSingleNumberExpression()
        {
            bool success = true;

            SaveToken();
            try
            {
                ExpressionRule_SingleNumberExpression();
            }
            catch(Exception ex)
            {
                success = false;
            }
            BackToken();

            return success;
        }

        //NUMBER
        private void ExpressionRule_SingleNumberExpression()
        {
            Match(TokenType.Number);
            Match(TokenType.EndOfStatement);
        }
        #endregion

        #region MultipleNumberExpression
        private bool ExpressionRule_IsMultipleNumberExpression()
        {
            bool success = true;

            SaveToken();
            try
            {
                ExpressionRule_MultipleNumberExpression();
            }
            catch (Exception ex)
            {
                success = false;
            }
            BackToken();

            return success;
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
        #endregion
        #endregion


        private void SaveToken()
        {
            this.savePoint_Position = astParser.GetPosition();
        }

        private void BackToken()
        {
            astParser.SetPosition(this.savePoint_Position-1);
            this.currentToken = astParser.Get();
        }

        private void MatchOne(TokenType tokenType, params TokenType[] tokenTypes)
        {
            List<TokenType> checkingTokenTypes = new List<TokenType>();
            checkingTokenTypes.Add(tokenType);

            if (tokenTypes != null)
                checkingTokenTypes.AddRange(tokenTypes);

            if (checkingTokenTypes.Contains(this.currentToken.Type))
            {
                ReadToken();
                return;
            }

            throw new InvalidOperationException();
        }

        private void Match(TokenType tokenType)
        {
            if (this.currentToken.Type == tokenType)
            {
                ReadToken();
                return;
            }

            throw new InvalidOperationException();
        }
    }
}
