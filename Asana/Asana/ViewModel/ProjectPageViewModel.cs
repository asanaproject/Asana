
using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class ProjectPageViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;
        private readonly IColumnService columnService;
        private readonly ITaskService taskService;
        public ProjectPageViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            columnService = new ColumnService();
            taskService = new TaskService();
            Columns = new ObservableCollection<ColumnItemViewModel>();
        }


        private ObservableCollection<ColumnItemViewModel> columns;
        public ObservableCollection<ColumnItemViewModel> Columns
        {
            get { return columns; }
            set { Set(ref columns,value); }
        }

 

        private RelayCommand<ColumnItemViewModel> createColumnCommand;
        public RelayCommand<ColumnItemViewModel> CreateColumnCommand => createColumnCommand ?? (createColumnCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            if (Columns.Count==0||!Columns.ToList().Exists(y=>!y.ColumnIsAdded))
            {
                Columns.Add(new ColumnItemViewModel());
            }
        }));
        private RelayCommand<ColumnItemViewModel> addColumnCommand;
        public RelayCommand<ColumnItemViewModel> AddColumnCommand => addColumnCommand ?? (addColumnCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            if (!String.IsNullOrWhiteSpace(x.Title))
            {
                Columns.First(y=>y==x).ColumnIsAdded=true;
            }
        }));

        private RelayCommand<ColumnItemViewModel> deleteColumnCommand;
        public RelayCommand<ColumnItemViewModel> DeleteColumnCommand => deleteColumnCommand ?? (deleteColumnCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            if (x!=null)
            {
               Columns.Remove(Columns.First(y => y == x));
            }
        }));

        private RelayCommand<ColumnItemViewModel> createTaskCommand;
        public RelayCommand<ColumnItemViewModel> CreateTaskCommand => createTaskCommand ?? (createTaskCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            x.Task = new Objects.Task { ColumnId = x.Column.Id };
        }));
    }
}
