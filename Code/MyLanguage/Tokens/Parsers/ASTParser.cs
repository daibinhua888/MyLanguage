using ConsoleApplication8.ASTrees;
using ConsoleApplication8.Tokens.Lexers;
using ConsoleApplication8.Tokens.Parsers.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers
{
    public class ASTParser
    {
        private TokenPool tokenPool;
        public Token currentToken;
        protected Stack<int> savedPositions=new Stack<int>();

        private StatementDetecter detecter;

        private IRule assignRule;
        private IRule blockRule;
        private IRule functionInvokeRule;
        private IRule ifRule;
        private IRule elseRule;
        private IRule whileRule;

        public ASTParser(Lexer tokenParser)
        {
            this.tokenPool = new TokenPool(tokenParser.GetAllTokens());

            this.detecter = new StatementDetecter(this);

            this.assignRule = new AssignRule(this);
            this.blockRule = new BlockRule(this);
            this.functionInvokeRule = new FunctionInvokeRule(this);
            this.ifRule = new IfRule(this);
            this.elseRule = new ElseRule(this);
            this.whileRule = new WhileRule(this);
        }

        public void Validate()
        {
            ReadToken();

            var stateType = this.detecter.DetectStatementType();

            RuleForward(stateType);
        }

        public AST ToAST()
        {
            ReadToken();

            var stateType = this.detecter.DetectStatementType();

            return RuleForward_AST(stateType);
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
        public void MatchOne_ButDontConsume(TokenType tokenType, params TokenType[] tokenTypes)
        {
            List<TokenType> checkingTokenTypes = new List<TokenType>();
            checkingTokenTypes.Add(tokenType);

            if (tokenTypes != null)
                checkingTokenTypes.AddRange(tokenTypes);

            if (checkingTokenTypes.Contains(this.currentToken.Type))
                return;

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

        public void Consume()
        {
            ReadToken();
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
                    return CanRuleForward(this.assignRule.Validate);
                    break;
                case StatementType.Block:
                    return CanRuleForward(this.blockRule.Validate);
                    break;
                case StatementType.FunctionInvoke:
                    return CanRuleForward(this.functionInvokeRule.Validate);
                    break;
                case StatementType.If:
                    return CanRuleForward(this.ifRule.Validate);
                    break;
                case StatementType.Else:
                    return CanRuleForward(this.elseRule.Validate);
                    break;
                case StatementType.While:
                    return CanRuleForward(this.whileRule.Validate);
                    break;
                default:
                    return false;
            }
        }


        public AST RuleForward_AST(StatementType stmtType)
        {
            switch (stmtType)
            {
                case StatementType.Assign:
                    return this.assignRule.AST();
                    break;
                case StatementType.FunctionInvoke:
                    return this.functionInvokeRule.AST();
                    break;
                case StatementType.Block:
                    return this.blockRule.AST();
                    break;
                case StatementType.If:
                    return this.ifRule.AST();
                    break;
                case StatementType.Else:
                    return this.elseRule.AST();
                    break;
                case StatementType.While:
                    return this.whileRule.AST();
                    break;
                default:
                    throw new InvalidOperationException("不支持的语句");
            }
        }
        public void RuleForward(StatementType stmtType)
        {
            switch (stmtType)
            {
                case StatementType.Assign:
                    this.assignRule.Validate();
                    break;
                case StatementType.FunctionInvoke:
                    this.functionInvokeRule.Validate();
                    break;
                case StatementType.Block:
                    this.blockRule.Validate();
                    break;
                case StatementType.If:
                    this.ifRule.Validate();
                    break;
                case StatementType.Else:
                    this.elseRule.Validate();
                    break;
                case StatementType.While:
                    this.whileRule.Validate();
                    break;
                default:
                    throw new InvalidOperationException("不支持的语句");
            }
        }
    }
}
