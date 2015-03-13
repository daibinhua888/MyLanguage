using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Lexers
{
    public class LexerProcessor
    {
        private string rawString;

        private LexerUtility helper = new LexerUtility();
        
        public LexerProcessor(string rawString)
        {
            this.rawString = rawString;
        }

        private int currentPos = -1;
        private bool alreadyEOF = false;
        private bool returnedEOF = false;

        private char ReadChar()
        {
            this.currentPos++;

            if (this.currentPos >= this.rawString.Length)
            {
                alreadyEOF = true;
                return (char)0;
            }

            return this.rawString[this.currentPos];
        }
        private void UnReadChar()
        {
            this.currentPos--;

            alreadyEOF = false;
        }

        #region Token parser
        public Token RecognizeToken()
        {
            if (alreadyEOF && returnedEOF)
                return new Token(TokenType.Invalid);

            var c = ReadChar();
            while (helper.IsWhiteSpace(c) && !helper.IsEOF(c))
                c = ReadChar();

            if (helper.IsEOF(c))                   //文件末尾
                return EOF();
            else if (helper.IsEOS(c))              //语句末尾
                return EOS();
            else if (helper.IsNumber(c))           //整形数字
                return Number(c);
            else if (helper.IsQuoteMark(c))        //双引号文本字符串
                return QuoteMark();
            else if (helper.IsLetter(c))           //标示符号（变量名、函数名、方法名、保留关键字）
                return Identifier(c);
            else if (helper.IsOperator(c))          //操作符号，比如+-*/、括号等
                return Operator(c);
            else
                throw new InvalidOperationException();
        }

        private Token Identifier(char c)
        {
            StringBuilder sb = new StringBuilder();

            while (helper.IsLetter(c) && !helper.IsEOF(c))
            {
                sb.Append(c);
                c = ReadChar();
            }

            if (!helper.IsLetter(c) && !helper.IsEOF(c))
                UnReadChar();

            return new Token(sb.ToString(), TokenType.Identifier);
        }

        private Token QuoteMark()
        {
            StringBuilder sb = new StringBuilder();

            char c = ReadChar();

            while (!helper.IsQuoteMark(c) && !helper.IsEOF(c))
            {
                sb.Append(c);
                c = ReadChar();
            }
            //此处不能调用UnReadChar函数，因为末尾的双引号需要被“吃”掉
            return new Token(sb.ToString(), TokenType.String);
        }

        private Token Number(char c)
        {
            StringBuilder sb = new StringBuilder();

            while (helper.IsNumber(c) && !helper.IsEOF(c))
            {
                sb.Append(c);
                c = ReadChar();
            }

            if (!helper.IsNumber(c) && !helper.IsEOF(c))
                UnReadChar();

            return new Token(sb.ToString(), TokenType.Number);
        }

        private Token Operator(char c)
        {
            if (c == '=')
                return new Token(TokenType.Equals);
            else if (c == '+')
                return new Token(TokenType.Plus);
            else if (c == '-')
                return new Token(TokenType.Minus);
            else if (c == '*')
                return new Token(TokenType.Multiply);
            else if (c == '/')
                return new Token(TokenType.Divide);
            else if (c == '(')
                return new Token(TokenType.LeftRoundBracket);
            else if (c == ')')
                return new Token(TokenType.RightRoundBracket);
            else if (c == '[')
                return new Token(TokenType.LeftBracket);
            else if (c == ']')
                return new Token(TokenType.RightBracket);
            else
                throw new Exception("无效操作符");
        }

        private Token EOS()
        {
            return new Token(TokenType.EndOfStatement);
        }

        private Token EOF()
        {
            returnedEOF = true;
            return new Token(TokenType.EndOfFile);
        }
        #endregion
    }
}
