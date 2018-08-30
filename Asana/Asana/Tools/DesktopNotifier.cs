using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;

namespace Asana.Tools
{
    public class DesktopNotifier
    {

        public static void NotifiPush(string photo, string body)
        {
            try
            {
                string path = FileHelper.GetPath("\\Apps\\DesktopNotifier");
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
                    proc.StartInfo.FileName = notifipush;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    proc.Start();
                    proc.WaitForExit();
                }
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }

        }
    }
}
