using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Tokens
{
    public enum TokenType
    {
        Invalid,
        EndOfFile,
        EndOfStatement,
        Number,             //  int
        String,             //  string
        Identifier,         //  变量名、方法名、保留关键字
        Equals,              //  =
        Plus,               //  +
        Minus,              //  -
        Multiply,           //  *
        Divide,             //  /
        LeftRoundBracket,   //  (
        RightRoundBracket,  //  )
        LeftBracket,        //  [
        RightBracket,       //  ]
        PlaceHolder,        //  占位符，无具体意义
    }
}
