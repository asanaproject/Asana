using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Tools
{
    public class DesktopNotifier
    {
        public static void NotifiPush(string photo, string body)
        {
            try
            {
                string path = FileHelper.GetPath("\\DesktopNotifier");
                string notifitextfile = path + "\\NotificationList.notifi";
                if (File.Exists(notifitextfile))
                {

                    File.WriteAllText(notifitextfile, String.Empty);
                    using (var writer = new StreamWriter(notifitextfile, true))
                    {
                        writer.Write(photo);
                        writer.Write("::Body::");
                        writer.Write(body);
                    }

                    string notifipush = path + "\\NotificationStatusBar.exe";
                    var proc = new Process();
                    proc.StartInfo.UseShellExecute =true;
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.FileName =notifipush;
                    proc.Start();
                }
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }

        }
    }
}
