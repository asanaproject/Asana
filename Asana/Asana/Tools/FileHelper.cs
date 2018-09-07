using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                return "";
            }
        }

        public static bool WriteBytesToFileWithStrin(byte[] htmlBytes)
        {
            try
            {
                //Process.Start("chrome.exe", GetPath("//Resources//mail.html"));
                string htmlString = Encoding.ASCII.GetString(htmlBytes);
                File.WriteAllText(GetPath("\\Resources\\mail.html"), htmlString);
                return true;
            }  
            catch(Exception err)
            {
                Log.Error(err.Message);
                return false;
            }
        }
        public static bool WriteTextToFile(string file)
        {
            try
            {
                File.WriteAllText(GetPath("\\Resources\\mail.html"), file);
                return true;
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return false;
            }
        }
    }
}
