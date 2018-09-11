using Asana.ViewModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class CurrentColumn: ViewModelBase
    {
        private CurrentColumn()
        {
            Column = new ColumnItemViewModel();
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
                return instance;
            }
        }
        private ColumnItemViewModel column;

        public ColumnItemViewModel Column
        {
            get { return column; }
            set {Set(ref column, value); }
        }

    }
}
