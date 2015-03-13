using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens.Lexers
{
    public class LexerUtility
    {
        public bool IsEOS(char c)
        {
            if (c == ';')
                return true;

            return false;
        }

        public bool IsEOF(char c)
        {
            if (c == 0)
                return true;

            return false;
        }

        public bool IsOperator(char c)
        {
            char[] operators = { '=', '+', '-', '*', '/', '(', ')', '[', ']' };

            if (operators.Contains(c))
                return true;

            return false;
        }

        public bool IsSeperator(char c)
        {
            char[] operators = { ','};

            if (operators.Contains(c))
                return true;

            return false;
        }

        public bool IsLetter(char c)
        {
            if (c >= 'a' && c <= 'z')
                return true;

            if (c >= 'A' && c <= 'Z')
                return true;

            return false;
        }

        public bool IsNumber(char c)
        {
            if (c >= '0' && c <= '9')
                return true;

            return false;
        }

        public bool IsWhiteSpace(char c)
        {
            if (c == ' ' || c == '\n' || c == '\r')
                return true;

            return false;
        }

        public bool IsQuoteMark(char c)
        {
            if (c == '"')
                return true;

            return false;
        }
    }
}
