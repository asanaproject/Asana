using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Tools
{
    public class Randomizer
    {
        public static string RandomKey { get; set; }

        public static void NextRandom()
        {
            try
            {
                List<string> keys = Guid.NewGuid().ToString().Split('-').ToList();
                RandomKey = keys[0].ToUpper();
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
        }
    }
}
