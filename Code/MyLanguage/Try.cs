using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    public class Try
    {
        public static Action OnException;

        public static void Execute(Action action)
        {
            try
            {
                action();
            }
            catch
            {
                if (OnException != null)
                    OnException();
                else
                    throw;
            }
        }
    }
}
