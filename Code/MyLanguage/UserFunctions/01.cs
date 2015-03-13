using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.UserFunctions
{
    class _01
    {
        public static void Show(params int[] lst)
        {
            Console.WriteLine("======Show结果====");

            if (lst != null && lst.Length > 0)
            {
                foreach (var i in lst)
                    Console.Write("{0}, ", i);
            }
            else
            {
                Console.Write("没有参数");
            }
            Console.WriteLine();
        }
    }
}
