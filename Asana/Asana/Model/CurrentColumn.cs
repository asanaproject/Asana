using Asana.Objects;
using Asana.Tools;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class CurrentColumn
    {
        private CurrentColumn(){
            //Column = new Column {ProjectId=3,Title="new column" };
        }
        private static CurrentColumn instance;
        public static CurrentColumn Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new CurrentColumn();
                }
                return Instance;
            }
        }
     


    }
}
