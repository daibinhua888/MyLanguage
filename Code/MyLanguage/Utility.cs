using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    public class Utility
    {
        public static void TryDo(Action action, Action<Exception> exceptionAction)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                exceptionAction(ex);
            }
        }
    }
}
