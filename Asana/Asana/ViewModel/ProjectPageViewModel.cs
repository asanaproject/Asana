
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
using Task = Asana.Objects.Task;

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
            StarPath = "../Resources/Images/grey_star.png";
        }
        public dynamic ColumnItem { get; set; }


        private ObservableCollection<ColumnItemViewModel> columns;
        public ObservableCollection<ColumnItemViewModel> Columns
        {
            get { return columns; }
            set { Set(ref columns, value); }
        }

        private RelayCommand createColumnCommand;
        public RelayCommand CreateColumnCommand => createColumnCommand ?? (createColumnCommand = new RelayCommand(
            () =>
            {
                if (!Columns.ToList().Exists(x => !x.ColumnIsAdded) || Columns.Count == 0)
                {
                    Columns.Add(new ColumnItemViewModel());

                }
            }

));



        private RelayCommand<ColumnItemViewModel> addColumnCommand;
        public RelayCommand<ColumnItemViewModel> AddColumnCommand => addColumnCommand ?? (addColumnCommand = new RelayCommand<ColumnItemViewModel>(
            x =>
            {
                if (!String.IsNullOrWhiteSpace(x.Title))
                {
                    // columnService.Add(new Column { Title = Title, ProjectId = 2 ,ColumnIsAdded=true});
                    Columns.First(y => y == x).ColumnIsAdded = true;
                }
            }
));

        private RelayCommand<ColumnItemViewModel> deleteColumnCommand;
        public RelayCommand<ColumnItemViewModel> DeleteColumnCommand => deleteColumnCommand ?? (deleteColumnCommand = new RelayCommand<ColumnItemViewModel>(
            x =>
            {
                if (x != null)
                {
                    Columns.Remove(Columns.First(y => y == x));
                }

            }
));

        private RelayCommand<ColumnItemViewModel> createTaskCommand;
        public RelayCommand<ColumnItemViewModel> CreateTaskCommand => createTaskCommand ?? (createTaskCommand = new RelayCommand<ColumnItemViewModel>(
            x =>
            {
                x.Task = new Task { ColumnId = x.Column.Id };
                
                x.Column.Tasks.Add(x.Task);
                CurrentTask.Instance.Task = x.Task;
                CurrentColumn.Instance.Column.Column = x.Column;
            }
));

        private string starPath;

        public string StarPath
        {
            get { return starPath; }
            set { Set(ref starPath, value); }
        }


        private RelayCommand<Task> addTaskCommand;
        public RelayCommand<Task> AddTaskCommand => addTaskCommand ?? (addTaskCommand = new RelayCommand<Task>(
            x =>
            {
                MessageBox.Show(x.Id.ToString());
                Columns.First(y => y.Column.Id == x.ColumnId).Task.IsTaskAdded = true;
            }
));
        private RelayCommand<Task> starTaskCommand;
        public RelayCommand<Task> StarTaskCommand => starTaskCommand ?? (starTaskCommand = new RelayCommand<Task>(
            x =>
            {

                x.IsStarred = x.IsStarred ? false : true;

                StarPath = x.IsStarred ? "../Resources/Images/yellow_star.png" : "../Resources/Images/grey_star.png";
            }
));


    }
}
