using ConsoleApplication8.ASTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Parsers.Rules
{
    public class FunctionInvokeRule : IRule
    {
        private ASTParser validator;

        public FunctionInvokeRule(ASTParser validator)
        {
            this.validator = validator;
        }

        public void Validate()
        {
            FunctionInvokeStatementRule();
        }

        private void FunctionInvokeStatementRule()
        {
            this.validator.Match(TokenType.Identifier);

            this.validator.MatchOne(TokenType.Identifier, TokenType.Number);

            while (this.validator.currentToken.Type != TokenType.EndOfStatement)
            {
                this.validator.Match(TokenType.Comma);
                this.validator.MatchOne(TokenType.Identifier, TokenType.Number);
            }

            this.validator.Match(TokenType.EndOfStatement);
        }


        public ASTrees.AST AST()
        {
            AST root = new AST(ASTTypes.CallStatement);

            AST methodNode = new AST(this.validator.currentToken, ASTTypes.Method);

            root.AddChild(methodNode);

            this.validator.Consume();       //consume variable token

            AST parametersNode = ParametersAST();

            root.AddChild(parametersNode);

            return root;
        }

        private ASTrees.AST ParametersAST()
        {
            AST parametersNode = new ASTrees.AST(ASTTypes.Parameters);

            AST parameterNode = RuleHelper.GetNumberOrVariableNode(this.validator);
            parametersNode.AddChild(parameterNode);

            while (this.validator.currentToken.Type != TokenType.EndOfStatement)
            {
                this.validator.Match(TokenType.Comma);

                parameterNode = RuleHelper.GetNumberOrVariableNode(this.validator);
                parametersNode.AddChild(parameterNode);
            }

            return parametersNode;
        }
    }
}
