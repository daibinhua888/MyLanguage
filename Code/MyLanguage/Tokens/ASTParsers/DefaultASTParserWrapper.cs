using ConsoleApplication8.ASTrees;
using ConsoleApplication8.Tokens.Lexers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.ASTParsers
{
    public class DefaultASTParserWrapper
    {
        private DefaultASTParser astParser;

        public DefaultASTParserWrapper(Lexer tokenParser)
        {
            this.astParser = new DefaultASTParser(tokenParser.GetAllTokens());
        }


        public AST ConstructAST()
        {
            var currentToken = astParser.Get();
            var nextToken = astParser.Peek(1);

            AST rootNode = null;

            if (currentToken.Type == TokenType.Identifier && nextToken.Type == TokenType.Equals)
            {
                //AssignStatement
                rootNode = ConstructAssignStatement();
            }
            else if (currentToken.Type == TokenType.Identifier && nextToken.Type != TokenType.Equals)
            {
                //MethodStatement
                rootNode = ConstructMethodStatement();
            }
            else
            {
                throw new InvalidOperationException("不支持的语句");
            }

            return rootNode;
        }

        private AST ConstructMethodStatement()
        {
            var rootNode = new AST(new Token(string.Empty, TokenType.PlaceHolder), ASTTypes.CallStatement);

            AST methodNode = new AST(new Token("show", TokenType.Identifier), ASTTypes.Method);

            AST parametersNode = new AST(new Token(string.Empty, TokenType.PlaceHolder), ASTTypes.Parameters);

            AST numberNode = new AST(new Token("a", TokenType.Identifier), ASTTypes.Variable);

            rootNode.AddChild(methodNode);
            rootNode.AddChild(parametersNode);

            parametersNode.AddChild(numberNode);

            return rootNode;
        }

        private AST ConstructAssignStatement()
        {
            var rootNode = new AST(new Token(TokenType.Equals), ASTTypes.AssignStatement);

            AST variableNode = new AST(new Token("a", TokenType.Identifier), ASTTypes.Variable);

            AST expressionNode = new AST(new Token(string.Empty, TokenType.PlaceHolder), ASTTypes.Expression);

            AST operatorNode = new AST(new Token(TokenType.Plus), ASTTypes.Operator);

            AST number1Node = new AST(new Token("100", TokenType.Number), ASTTypes.NumberLiteral);
            AST number2Node = new AST(new Token("200", TokenType.Number), ASTTypes.NumberLiteral);

            rootNode.AddChild(variableNode);
            rootNode.AddChild(expressionNode);

            expressionNode.AddChild(operatorNode);

            operatorNode.AddChild(number1Node);
            operatorNode.AddChild(number2Node);

            return rootNode;
        }
    }
}
