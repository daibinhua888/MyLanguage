using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.RuntimeEnviroment
{
    public class SymbolTable
    {
        private Dictionary<string, object> table = new Dictionary<string, object>();

        public List<string> AllKeys { get { return this.table.Keys.ToList(); } }

        public void Put(string variableName, object value)
        {
            table[variableName] = value;
        }

        public object Get(string variableName)
        {
            return table[variableName];
        }

        public int GetInt32(string variableName)
        {
            return (int)Get(variableName);
        }
    }
}
