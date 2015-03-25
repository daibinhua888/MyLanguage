using ConsoleApplication8.ASTrees;
using ConsoleApplication8.RuntimeEnviroment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Interpreters
{
    class ASTProcessor
    {
        private AST ast;
        private SymbolTable symbolTable;

        internal void Handle(ASTrees.AST ast, RuntimeEnviroment.SymbolTable symbolTable)
        {
            this.ast = ast;
            this.symbolTable = symbolTable;

            Eval(ast);
        }

        private object Eval(AST ast)
        {
            object value=null;
            object leftValue = null;
            object rightValue = null;

            switch(ast.ASTType)
            {
                case ASTTypes.AssignStatement:
                    string variableName = GetVariableName(ast.Children[0]);
                    value = Eval(ast.Children[1]);
                    symbolTable.Put(variableName, value);
                    break;
                case ASTTypes.Variable:
                    value = symbolTable.Get(ast.Token.Text);
                    break;
                case ASTTypes.Expression:
                    value = Eval(ast.Children[0]);
                    break;
                case ASTTypes.Operator:
                    switch(ast.Token.Type)
                    {
                        case TokenType.Plus:
                            leftValue = Eval(ast.Children[0]);
                            rightValue = Eval(ast.Children[1]);
                            value = Convert.ToInt32(leftValue) + Convert.ToInt32(rightValue);
                            break;
                        case TokenType.Minus:
                            leftValue = Eval(ast.Children[0]);
                            rightValue = Eval(ast.Children[1]);
                            value = Convert.ToInt32(leftValue) - Convert.ToInt32(rightValue);
                            break;
                        case TokenType.Multiply:
                            leftValue = Eval(ast.Children[0]);
                            rightValue = Eval(ast.Children[1]);
                            value = Convert.ToInt32(leftValue) * Convert.ToInt32(rightValue);
                            break;
                        case TokenType.Divide:
                            leftValue = Eval(ast.Children[0]);
                            rightValue = Eval(ast.Children[1]);
                            value = Convert.ToInt32(leftValue) / Convert.ToInt32(rightValue);
                            break;
                        default:
                            throw new InvalidOperationException();
                            break;
                    }
                    break;
                case ASTTypes.NumberLiteral:
                    value = Convert.ToInt32(ast.Token.Text);
                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }

            return value;
        }

        private string GetVariableName(AST ast)
        {
            string name = string.Empty;
            switch(ast.ASTType)
            {
                case ASTTypes.Variable:
                    name = ast.Token.Text;
                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }
            return name;
        }
    }
}
