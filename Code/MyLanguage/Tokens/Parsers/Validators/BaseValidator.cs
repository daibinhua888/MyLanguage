using ConsoleApplication8.Tokens.Lexers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Validators
{
    public abstract class BaseValidator
    {
        private TokenPool tokenPool;
        protected Token currentToken;
        protected Stack<int> savePoint_Position=new Stack<int>();

        public BaseValidator(Lexer tokenParser)
        {
            this.tokenPool = new TokenPool(tokenParser.GetAllTokens());
        }

        public abstract void Validate();

        protected Token PeekToken(int position)
        {
            return tokenPool.Peek(position);
        }

        protected void ReadToken()
        {
            this.currentToken = tokenPool.Get();
        }

        protected void SaveToken()
        {
            this.savePoint_Position.Push(tokenPool.GetPosition());
        }

        protected void BackToken()
        {
            int newPosition = this.savePoint_Position.Pop();
            tokenPool.SetPosition(newPosition - 1);
            this.currentToken = tokenPool.Get();
        }

        protected void MatchOne(TokenType tokenType, params TokenType[] tokenTypes)
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

        protected void Match(TokenType tokenType)
        {
            if (this.currentToken.Type == tokenType)
            {
                ReadToken();
                return;
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// 测试规则是否可行
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        protected bool CanRuleForward(Action rule)
        {
            bool success = true;

            SaveToken();
            try
            {
                rule();
            }
            catch (Exception ex)
            {
                success = false;
            }
            BackToken();

            return success;
        }
    }
}
