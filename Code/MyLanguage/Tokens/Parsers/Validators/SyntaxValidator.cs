using ConsoleApplication8.Tokens.Lexers;
using ConsoleApplication8.Tokens.Parsers.Validators.ConcreteRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Validators
{
    public class SyntaxValidator
    {
        private TokenPool tokenPool;
        public Token currentToken;
        protected Stack<int> savedPositions=new Stack<int>();

        private StatementDetecter detecter;

        private IRuleProcessor assignRuleProcessor;
        private IRuleProcessor blockRuleProcessor;
        private IRuleProcessor functionInvokeProcessor;
        private IRuleProcessor ifRuleProcessor;
        private IRuleProcessor elseRuleProcessor;
        private IRuleProcessor whileRuleProcessor;

        public SyntaxValidator(Lexer tokenParser)
        {
            this.tokenPool = new TokenPool(tokenParser.GetAllTokens());

            this.detecter = new StatementDetecter(this);

            this.assignRuleProcessor = new AssignRule(this);
            this.blockRuleProcessor = new BlockRule(this);
            this.functionInvokeProcessor = new FunctionInvokeRule(this);
            this.ifRuleProcessor = new IfRule(this);
            this.elseRuleProcessor = new ElseRule(this);
            this.whileRuleProcessor = new WhileRule(this);
        }

        public void Validate()
        {
            ReadToken();

            var stateType = this.detecter.DetectStatementType();

            RuleForward(stateType);
        }

        public Token PeekToken(int position)
        {
            return tokenPool.Peek(position);
        }

        protected void ReadToken()
        {
            this.currentToken = tokenPool.Get();
        }

        protected void SaveToken()
        {
            this.savedPositions.Push(tokenPool.GetPosition());
        }

        protected void BackToken()
        {
            int newPosition = this.savedPositions.Pop();
            tokenPool.SetPosition(newPosition - 1);
            this.currentToken = tokenPool.Get();
        }

        public void MatchOne(TokenType tokenType, params TokenType[] tokenTypes)
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

        public void MatchAnd(TokenType tokenType, string tokenText)
        {
            if (this.currentToken.Type == tokenType&&this.currentToken.Text==tokenText)
            {
                ReadToken();
                return;
            }

            throw new InvalidOperationException();
        }

        public void Match(TokenType tokenType)
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
        public bool CanRuleForward(Action rule)
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

        public bool CanRuleForward(StatementType stmtType)
        {
            switch (stmtType)
            {
                case StatementType.Assign:
                    return CanRuleForward(this.assignRuleProcessor.ProcessRule);
                    break;
                case StatementType.Block:
                    return CanRuleForward(this.blockRuleProcessor.ProcessRule);
                    break;
                case StatementType.FunctionInvoke:
                    return CanRuleForward(this.functionInvokeProcessor.ProcessRule);
                    break;
                case StatementType.If:
                    return CanRuleForward(this.ifRuleProcessor.ProcessRule);
                    break;
                case StatementType.Else:
                    return CanRuleForward(this.elseRuleProcessor.ProcessRule);
                    break;
                case StatementType.While:
                    return CanRuleForward(this.whileRuleProcessor.ProcessRule);
                    break;
                default:
                    return false;
            }
        }

        public void RuleForward(StatementType stmtType)
        {
            switch (stmtType)
            {
                case StatementType.Assign:
                    this.assignRuleProcessor.ProcessRule();
                    break;
                case StatementType.FunctionInvoke:
                    this.functionInvokeProcessor.ProcessRule();
                    break;
                case StatementType.Block:
                    this.blockRuleProcessor.ProcessRule();
                    break;
                case StatementType.If:
                    this.ifRuleProcessor.ProcessRule();
                    break;
                case StatementType.Else:
                    this.elseRuleProcessor.ProcessRule();
                    break;
                case StatementType.While:
                    this.whileRuleProcessor.ProcessRule();
                    break;
                default:
                    throw new InvalidOperationException("不支持的语句");
            }
        }
    }
}
