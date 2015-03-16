﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Validators.ConcreteRules
{
    public class IfRule : IRuleProcessor
    {
        private SyntaxValidator validator;

        public IfRule(SyntaxValidator validator)
        {
            this.validator = validator;
        }

        public void ProcessRule()
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
    }
}
