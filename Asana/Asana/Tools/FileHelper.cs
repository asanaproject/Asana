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
                string text = reader.ReadToEnd();
                reader.Dispose();
                return text;
            }
            catch(Exception err)
            {
                Log.Error(err.Message);
                return "";
            }
        }
        public static string GetPath(string path)
        {
            try
            {
                string pathfile = Assembly.GetExecutingAssembly().Location + "\\..\\..\\.." + path;
                return pathfile;
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return "";
            }
        }

        public static string GetTextFromFile(string path)
        {
            try
            {
                StreamReader reader = new StreamReader(path);
                string text = reader.ReadToEnd();
                reader.Dispose();
                return text;
            }
            catch(Exception err)
            {
                Log.Error(err.Message);
                return "";            }
        }
    }
}
