using Asana.Objects;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Asana.Objects.Task;

namespace Asana.ViewModel
{
    public class ColumnItemViewModel:ViewModelBase
    {
       
        private Column column;
        public Column Column
        {
            get { return column; }
            set { Set(ref column,value); }
        }

        public ColumnItemViewModel()
        {
            Column = new Column();
            Task = new Task();
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { Set(ref title,value); }
        }
        private bool columnIsAdded;

        public bool ColumnIsAdded
        {
            get { return columnIsAdded; }
            set { Set(ref columnIsAdded,value); }
        }
      
        private Task task;
        public Task Task
        {
            get { return task; }
            set {Set(ref task, value); }
        }

    }
}
