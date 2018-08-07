using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    public class HtmlParser
    {
        public static string InsertInto(char symbol,string html)
        {
            Randomizer.NextRandom();
            string key = Randomizer.RandomKey;
            int index = html.IndexOf(symbol);
            return html.Substring(0, index) + key + html.Substring(index+1, html.Length - (index+1));
        }
    }
}
