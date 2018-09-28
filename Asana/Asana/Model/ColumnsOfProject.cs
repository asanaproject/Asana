using Asana.ViewModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public  class ColumnsOfProject:ViewModelBase
    {
        private ColumnsOfProject(){
            Columns = new ObservableCollection<ColumnItemViewModel>();
        }
        private static ColumnsOfProject instance;
        public static ColumnsOfProject Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new ColumnsOfProject();
                }
                return instance;
            }
        }
        private  ObservableCollection<ColumnItemViewModel> columns;
        public  ObservableCollection<ColumnItemViewModel> Columns
        {
            get { return columns; }
            set { Set(ref columns,value); }
        }

    }
}
