
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

        private RelayCommand createColumnCommand;
        public RelayCommand CreateColumnCommand => createColumnCommand ?? (createColumnCommand = new RelayCommand(
            () =>
            {
               Columns.Add(new ColumnItemViewModel());

            }

));


    }
}
