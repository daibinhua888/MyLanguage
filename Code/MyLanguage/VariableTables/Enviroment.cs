using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.VariableTables
{
    public class Enviroment
    {
        private Dictionary<string, object> values=new Dictionary<string,object>();

        public void Set(string variableName, object value)
        {
            values[variableName] = value;
        }

        public object Get(string variableName)
        {
            return values[variableName];
        }
    }
}
