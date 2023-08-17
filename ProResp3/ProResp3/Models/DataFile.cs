using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProResp3.Models
{
    using System.IO;
    public class DataFile
    {
        string path;

        public DataFile(string argPath)
        {
            path = argPath;
        }

        public void writeToFile(string data, bool append)
        {
            using (StreamWriter sw = new StreamWriter(path, append))
            {
                sw.WriteLine(data);
                sw.Close();
            }
        }
    }
}
