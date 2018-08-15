using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Tools
{
    public class FileHelper
    {
        public static string FindFile(string path)
        {
            try
            {
                string pathfile = Assembly.GetExecutingAssembly().Location + "\\..\\..\\.." + path;
                StreamReader reader = new StreamReader(pathfile);
                return reader.ReadToEnd();
            }
            catch(Exception err)
            {
                Log.Error(err.Message);
                return "";
            }
        }
    }
}
