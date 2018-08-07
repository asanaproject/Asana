using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    public class FileHelper
    {
        public static string FindFile(string path)
        {
            string pathfile = Assembly.GetExecutingAssembly().Location + "\\..\\..\\.." + path;
            StreamReader reader = new StreamReader(pathfile);
            return reader.ReadToEnd();
        }
    }
}
