using Asana.Objects;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Asana.Model
{
    public class CurrentTask:ViewModelBase
    {
        private CurrentTask()
        {
            Task = new Task();
        }
        private static CurrentTask instance;
        public static CurrentTask Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new CurrentTask();
                }
                return instance;
            }
        }
        private Task task;
        public Task Task
        {
            get { return task; }
            set {Set(ref task,value); }
        }

    }
}
