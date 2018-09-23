using Asana.Objects;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class CurrentProject:ViewModelBase
    {
        private CurrentProject(){
            Project = new Project();
        }
        private static CurrentProject instance;
        public static CurrentProject Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new CurrentProject();
                }
                return instance;
            }
        }
         

        private Project project;
        public Project Project
        {
            get { return project; }
            set { project = value;   }
        }

      

    }
}
