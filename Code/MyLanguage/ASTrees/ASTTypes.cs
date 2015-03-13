using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.ASTrees
{
    public enum ASTTypes
    {
        AssignStatement,        //a=1+2;
        CallStatement,          //show 100;
        IfStatement,            /*
                                        if a>100
                                                show 1;
                                                show 2;
                                        elseif a>50
                                                show 3;
                                        elseif a>10 and b=1
                                                show 4;
                                        else
                                                show 0;
                                        endif
                                */
        BlockStatement,
        WhileStatement,         /*
                                        while a<100
                                            a=a+1;
                                            show a;
                                        endwhile
                                */

        StringLiteral,
        NumberLiteral,

        Expression,
        Parameters,

        Variable,               //表示变量

        Method,                 //表示方法调用

        ConditionBranch,        //用于if/elseif/elseif的条件
        ElseBranch,             //用于else分支
        Operator                // +/-/*//等
    }
}
