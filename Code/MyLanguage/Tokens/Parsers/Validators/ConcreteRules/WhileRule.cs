using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Validators.ConcreteRules
{
    public class WhileRule : IRuleProcessor
    {
        private SyntaxValidator validator;

        public WhileRule(SyntaxValidator validator)
        {
            this.validator = validator;
        }

        public void ProcessRule()
        {
            WhileStatementRule();
        }

        private void WhileStatementRule()
        {
        }
    }
}
