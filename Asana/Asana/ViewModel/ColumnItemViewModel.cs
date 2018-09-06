using Asana.Model;
using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task = Asana.Objects.Task;

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
            ColumnIsAdded = false;
        }
        private string title;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }
        private bool columnIsAdded;

        public bool ColumnIsAdded
        {
            get { return columnIsAdded; }
            set { Set(ref columnIsAdded,value); }
        }

        private RelayCommand addColumnCommand;
        public RelayCommand AddColumnCommand => addColumnCommand ?? (addColumnCommand = new RelayCommand(
            () =>
            {
                if (!String.IsNullOrWhiteSpace(Title))
                {
                   // columnService.Add(new Column { Title = Title, ProjectId = 2 ,ColumnIsAdded=true});
                    ColumnIsAdded = true;
                }
            }
));


        private RelayCommand columnSettingCommand;
        public RelayCommand ColumnSettingCommand => columnSettingCommand ?? (columnSettingCommand = new RelayCommand(
            () =>
            {
                if (!String.IsNullOrWhiteSpace(Title))
                {
                    // columnService.Add(new Column { Title = Title, ProjectId = 2 ,ColumnIsAdded=true});
                    ColumnIsAdded = true;
                }
            }
));
        private RelayCommand addTaskCommand;
        public RelayCommand AddTaskCommand => addTaskCommand ?? (addTaskCommand = new RelayCommand(
            () =>
            {
                Column.Tasks.Add(new Task());
            }
));
    }
}
