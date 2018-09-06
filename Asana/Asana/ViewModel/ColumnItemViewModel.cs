using Asana.Model;
using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class ColumnItemViewModel : ViewModelBase
    {
        private readonly IColumnService columnService;
        private Column column;
        public Column Column
        {
            get { return column; }
            set { Set(ref column, value); }
        }
        public ColumnItemViewModel()
        {
            Column = new Column { ProjectId = 2 };
            columnService = new ColumnService();
        }
        private string title;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }
        private RelayCommand addColumnCommand;
        public RelayCommand AddColumnCommand => addColumnCommand ?? (addColumnCommand = new RelayCommand(
            () =>
            {
                if (!String.IsNullOrWhiteSpace(Title))
                {
                    columnService.Add(new Column { Title = Title, ProjectId = 2 ,ColumnIsAdded=true});
                }
            }
));


      
    }
}
