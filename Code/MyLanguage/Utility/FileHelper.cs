using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8.Utility
{
    class FileHelper
    {
        public static void Save(string s, string path)
        {
            using (FileStream fout = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (StreamWriter brout = new StreamWriter(fout, Encoding.Default))
            {
                brout.Write(s);
                brout.Close();
            }
        }
    }
}
