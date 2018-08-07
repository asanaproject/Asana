using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    public class Randomizer
    {
        public static string RandomKey { get; set; }

        public static void NextRandom()
        {
            List<string> keys = Guid.NewGuid().ToString().Split('-').ToList();
            RandomKey = keys[0].ToUpper();
        }
    }
}
