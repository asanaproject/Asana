using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asana.Model
{
    public class CurrentTask
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
        public Task Task { get; set; }
    }
}
