using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Tools
{
    public class Settings
    {
        public static void SetDefaultSettings()
        {
            try
            {
                //Connecting Logger to Slack
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(@"https://hooks.slack.com/services/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")
                        .CreateLogger();
                //Others
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
        }
    }
