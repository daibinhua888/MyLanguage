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

        /// <summary>
        /// 等于号（赋值等于号）
        /// </summary>
        Equals,              //  =

        /// <summary>
        /// 比较等于号
        /// </summary>
        CompareEquals,      //   ==

        Plus,               //  +
        Minus,              //  -
        Multiply,           //  *
        Divide,             //  /

        /// <summary>
        /// 左圆括号
        /// </summary>
        LeftRoundBracket,   //  (

        /// <summary>
        /// 右圆括号
        /// </summary>
        RightRoundBracket,  //  )

        /// <summary>
        /// 左中括号
        /// </summary>
        LeftBracket,        //  [

        /// <summary>
        /// 右中括号
        /// </summary>
        RightBracket,       //  ]

        /// <summary>
        /// 左花括号
        /// </summary>
        LeftBrace,          //  {

        /// <summary>
        /// 右花括号
        /// </summary>
        RightBrace,         //  }

        /// <summary>
        /// 逗号
        /// </summary>
        Comma,              //  ,
        PlaceHolder,        //  占位符，无具体意义
    }
}
