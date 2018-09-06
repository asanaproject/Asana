using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class CurrentProject
    {
        private  CurrentProject() { }
        private static CurrentProject instance;
        public static CurrentProject Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new CurrentProject();
                }
                return Instance;
            }
        }
        public Project Project { get; set; }
    }
}
