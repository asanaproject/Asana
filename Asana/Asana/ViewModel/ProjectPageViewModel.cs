
using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GongSolutions.Wpf.DragDrop;
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
            Header = new HeaderViewModel(navigation);
            StarPath = "../Resources/Images/grey_star.png";
        }

        private HeaderViewModel header;
        public HeaderViewModel Header
        {
            get { return header; }
            set { Set(ref header, value); }
        }


        private ObservableCollection<ColumnItemViewModel> columns;
        public ObservableCollection<ColumnItemViewModel> Columns
        {
            get { return columns; }
            set { Set(ref columns, value); }
        }

        private string starPath;
        public string StarPath
        {
            get { return starPath; }
            set { Set(ref starPath, value); }
        }


        /// <summary>
        /// creating empty template for column 
        /// </summary>
        private RelayCommand<ColumnItemViewModel> createColumnCommand;
        public RelayCommand<ColumnItemViewModel> CreateColumnCommand => createColumnCommand ?? (createColumnCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            if (Columns.Count == 0 || !Columns.ToList().Exists(y => !y.ColumnIsAdded))
            {
                Columns.Add(new ColumnItemViewModel());
            }
        }));

        /// <summary>
        /// making task priority
        /// </summary>
        private RelayCommand<Task> starTaskCommand;
        public RelayCommand<Task> StarTaskCommand => starTaskCommand ?? (starTaskCommand = new RelayCommand<Task>(
        x =>
        {
            x.IsStarred = x.IsStarred ? false : true;
            StarPath = x.IsStarred ? "../Resources/Images/star-icon.png" : "../Resources/Images/grey_star.png";
        }));

        /// <summary>
        /// adding column inside project
        /// </summary>
        private RelayCommand<ColumnItemViewModel> addColumnCommand;
        public RelayCommand<ColumnItemViewModel> AddColumnCommand => addColumnCommand ?? (addColumnCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            if (!String.IsNullOrWhiteSpace(x.Title))
            {
                Columns.First(y => y == x).ColumnIsAdded = true;
            }
        }));


        /// <summary>
        /// removing column from column list of project
        /// </summary>
        private RelayCommand<ColumnItemViewModel> deleteColumnCommand;
        public RelayCommand<ColumnItemViewModel> DeleteColumnCommand => deleteColumnCommand ?? (deleteColumnCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            if (x != null)
            {
                Columns.Remove(Columns.First(y => y == x));
            }
        }));

        /// <summary>
        /// creating empty template for task inside column
        /// </summary>
        private RelayCommand<ColumnItemViewModel> createTaskCommand;
        public RelayCommand<ColumnItemViewModel> CreateTaskCommand => createTaskCommand ?? (createTaskCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            if (!Columns.First(z => z.Column.Id == x.Column.Id).Column.Tasks.ToList().Exists(k => !k.IsTaskAdded) ||
                Columns.First(z => z.Column.Id == x.Column.Id).Column.Tasks.Count == 0)
            {
                x.Task = new Objects.Task { ColumnId = x.Column.Id };
                Columns.ToList().First(y => y.Column.Id == x.Column.Id).Column.Tasks.Add(x.Task);
            }
        }));

        /// <summary>
        /// Adding task inside column
        /// </summary>
        private RelayCommand<Task> addTaskCommand;
        public RelayCommand<Task> AddTaskCommand => addTaskCommand ?? (addTaskCommand = new RelayCommand<Task>(
        x =>
        {
            if (!String.IsNullOrWhiteSpace(x.Title))
            {
                Columns.First(y => y.Column.Id == x.ColumnId).Column.Tasks.First(z => z.Id == x.Id).IsTaskAdded = true;
                Columns.First(y => y.Column.Id == x.ColumnId).Column.Tasks.First(z => z.Id == x.Id).Title = x.Title;
            }

        }));

        /// <summary>
        /// removing task from task list of column
        /// </summary>
        private RelayCommand<Task> discardTaskCommand;
        public RelayCommand<Task> DiscardTaskCommand => discardTaskCommand ?? (discardTaskCommand = new RelayCommand<Task>(
        x =>
        {
            MessageBox.Show(x.Id.ToString());
            if (x!=null)
            {
                Columns.First(y => y.Column.Id == x.ColumnId).Column.Tasks.Remove(Columns.First(y => y.Column.Id == x.ColumnId).Column.Tasks.First(z => z.Id == x.Id));
            }

        }));


        /// <summary>
        /// 
        /// </summary>
        private RelayCommand<Task> editTaskCommand;
        public RelayCommand<Task> EditTaskCommand => editTaskCommand ?? (editTaskCommand = new RelayCommand<Task>(
        x =>
        {
            if (x != null)
            {
                Columns.First(y => y.Column.Id == x.ColumnId).Column.Tasks.First(z => z.Id == x.Id).IsTaskAdded = false;
            }

        }));

     
    }
}
