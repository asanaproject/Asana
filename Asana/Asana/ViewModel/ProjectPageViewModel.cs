
using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using Asana.Tools;
using Asana.View;
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
    public class ProjectPageViewModel : ViewModelBase, IDropTarget
    {
        private readonly NavigationService navigation;
        private readonly IColumnService columnService;
        private readonly ITaskService taskService;
        private readonly IProjectService projectService;

        public ProjectPageViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            columnService = new ColumnService();
            taskService = new TaskService();
            projectService = new ProjectService();
            Header = new HeaderViewModel(navigation);
        }

        private HeaderViewModel header;
        public HeaderViewModel Header
        {
            get { return header; }
            set { Set(ref header, value); }
        }


        private ObservableCollection<KanbanState> kanbanStates;
        public ObservableCollection<KanbanState> KanbanStates
        {
            get { return kanbanStates; }
            set { Set(ref kanbanStates, value); }
        }
        private UserRoles assignedTo;

        public UserRoles AssignedTo
        {
            get { return assignedTo; }
            set { Set(ref assignedTo, value); }
        }



        /// <summary>
        /// creating empty template for column 
        /// </summary>
        private RelayCommand<ColumnItemViewModel> createColumnCommand;
        public RelayCommand<ColumnItemViewModel> CreateColumnCommand => createColumnCommand ?? (createColumnCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            if (ColumnsOfProject.Instance.Columns.Count == 0 ||
               !ColumnsOfProject.Instance.Columns.ToList().Exists(y => !y.ColumnIsAdded))
            {
                ColumnsOfProject.Instance.Columns.Add(new ColumnItemViewModel());
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
            x.StarPath = x.IsStarred ? "../Resources/Images/star-icon.png" : "../Resources/Images/grey_star.png";
            taskService.UpdateAsync(x);
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
                x.ColumnIsAdded = true;
                x.Column.CreatedAt = DateTime.Now;
                x.Column.ProjectId = CurrentProject.Instance.Project.Id;
                x.Column.Title = x.Title;
                x.Column.IsColumnAdded = true;
                columnService.CreateAsync(x.Column);
                columnService.LoadColumns(CurrentProject.Instance.Project.Id);
                columnService.LoadColumns(CurrentProject.Instance.Project.Id);
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
                if (x.Column.IsColumnAdded)
                {
                    columnService.RemoveAsync(x.Column);
                }
                columnService.LoadColumns(CurrentProject.Instance.Project.Id);
                columnService.LoadColumns(CurrentProject.Instance.Project.Id);


            }
        }));

        /// <summary>
        /// creating empty template for task inside column
        /// </summary>
        private RelayCommand<ColumnItemViewModel> createTaskCommand;
        public RelayCommand<ColumnItemViewModel> CreateTaskCommand => createTaskCommand ?? (createTaskCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            if (!ColumnsOfProject.Instance.Columns.First(z => z.Column.Id == x.Column.Id).Column.Tasks.ToList().Exists(k => !k.IsTaskAdded) ||
                ColumnsOfProject.Instance.Columns.First(z => z.Column.Id == x.Column.Id).Column.Tasks.Count == 0)
            {
                x.Task = new Objects.Task { ColumnId = x.Column.Id };
                ColumnsOfProject.Instance.Columns.ToList().First(y => y.Column.Id == x.Column.Id).Column.Tasks.Add(x.Task);
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
                KanbanStates = new ObservableCollection<KanbanState>(taskService.GetKanbanStatesOfTask());
                MessageBox.Show(x.AssignedTo);

                if (AssignedTo != null)
                {
                    x.IsTaskAdded = true;
                    x.CreatedAt = DateTime.Now;
                    x.AssignedTo = AssignedTo.FullName;
                    MessageBox.Show(x.AssignedTo);
                    taskService.CreateAsync(x);
                }
            }

        }));

        /// <summary>
        /// removing task from task list of column
        /// </summary>
        private RelayCommand<Task> discardTaskCommand;
        public RelayCommand<Task> DiscardTaskCommand => discardTaskCommand ?? (discardTaskCommand = new RelayCommand<Task>(
        x =>
        {
            if (x != null)
            {
                if (x.IsTaskAdded)
                {
                    taskService.RemoveAsync(x);
                }
                columnService.LoadColumns(CurrentProject.Instance.Project.Id);
            }

        }));


        /// <summary>
        /// editing selected task
        /// </summary>
        private RelayCommand<Task> editTaskCommand;
        public RelayCommand<Task> EditTaskCommand => editTaskCommand ?? (editTaskCommand = new RelayCommand<Task>(
        x =>
        {
            CurrentTask.Instance.Task = x;
            WindowBluringCustom.Bluring();
            ExtraWindow extraWindow = new ExtraWindow(new EditTaskViewModel(navigation), 800, 450);
            extraWindow.ShowDialog();
            WindowBluringCustom.Normal();

        }));

        /// <summary>
        /// this command is for showing example column/task which user should create
        /// </summary>
        private RelayCommand<ColumnItemViewModel> showExampleCommand;
        public RelayCommand<ColumnItemViewModel> ShowExampleCommand => showExampleCommand ?? (showExampleCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            WindowBluringCustom.Bluring();
            ExtraWindow extraWindow = new ExtraWindow(new KanbanExamplesViewModel(navigation), 800, 450);
            extraWindow.ShowDialog();
            WindowBluringCustom.Normal();

        }));


        /// <summary>
        /// editing properties of selected column
        /// </summary>
        private RelayCommand<ColumnItemViewModel> editColumnCommand;
        public RelayCommand<ColumnItemViewModel> EditColumnCommand => editColumnCommand ?? (editColumnCommand = new RelayCommand<ColumnItemViewModel>(
        x =>
        {
            CurrentColumn.Instance.Column = x;
            WindowBluringCustom.Bluring();
            ExtraWindow extraWindow = new ExtraWindow(new EditColumnViewModel(navigation), 400, 200);
            extraWindow.ShowDialog();
            WindowBluringCustom.Normal();

        }));


        private RelayCommand assignToNewUserCommand;
        public RelayCommand AssignToNewUserCommand => assignToNewUserCommand ?? (assignToNewUserCommand = new RelayCommand(
       () =>
       {
           WindowBluringCustom.Bluring();
           ExtraWindow extraWindow = new ExtraWindow(new AssignToNewUserViewModel(navigation), 600, 350);
           extraWindow.ShowDialog();
           WindowBluringCustom.Normal();

       }

       ));

        private RelayCommand<Task> selectTaskCommand;
        public RelayCommand<Task> SelectTaskCommand => selectTaskCommand ?? (selectTaskCommand = new RelayCommand<Task>(
        x =>
        {
            if (x != null && x.IsTaskAdded)
            {
                CurrentTask.Instance.Task = x;

                WindowBluringCustom.Bluring();
                ExtraWindow extraWindow = new ExtraWindow(new TaskPageViewModel(navigation), 700, 350);
                extraWindow.ShowDialog();
                WindowBluringCustom.Normal();
            }
        }));

        private RelayCommand<UserRoles> assignTaskCommand;
        public RelayCommand<UserRoles> AssignTaskCommand => assignTaskCommand ?? (assignTaskCommand = new RelayCommand<UserRoles>(
        x =>
        {
            MessageBox.Show("rfr");
            if (x != null)
            {
                AssignedTo = x;
            }

        }));


        private RelayCommand<KanbanState> selectionChangedCommand;
        public RelayCommand<KanbanState> SelectionChangedCommand => selectionChangedCommand ?? (selectionChangedCommand = new RelayCommand<KanbanState>(
        x =>
        {
            if (x == null)
            {
                var state = new TaskKanbanState
                {
                    KanbanState = x,
                    KanbanStateId = x.Id,
                    ChangedBy = CurrentUser.Instance.User.FullName,
                    Date = DateTime.Now,
                    Task = CurrentTask.Instance.Task,
                    TaskId = CurrentTask.Instance.Task.Id
                };
                taskService.UpdateAsyncKanbanState(CurrentTask.Instance.Task, state);
                columnService.LoadColumns(CurrentProject.Instance.Project.Id);
            }
        }));


        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            dropInfo.Effects = DragDropEffects.Move | DragDropEffects.Copy;

        }

        public void Drop(IDropInfo dropInfo)
        {
            ColumnItemViewModel sourceItem = dropInfo.Data as ColumnItemViewModel;
            ColumnItemViewModel targetItem = dropInfo.TargetItem as ColumnItemViewModel;
            if (sourceItem != null && targetItem != null)
            {

                var sourceIndex = ColumnsOfProject.Instance.Columns.IndexOf(sourceItem);
                var targetIndex = ColumnsOfProject.Instance.Columns.IndexOf(targetItem);
                if (sourceIndex != targetIndex)
                {
                    columnService.UpdateAsync(targetIndex, sourceIndex, targetItem.Column);
                    columnService.UpdateAsync(sourceIndex, targetIndex, sourceItem.Column);
                    columnService.LoadColumns(CurrentProject.Instance.Project.Id);
                    //ColumnsOfProject.Instance.Columns.RemoveAt(sourceIndex);
                    //ColumnsOfProject.Instance.Columns.Insert(sourceIndex, targetItem);
                    //ColumnsOfProject.Instance.Columns.RemoveAt(targetIndex);
                    //ColumnsOfProject.Instance.Columns.Insert(targetIndex, sourceItem);

                }
            }
        }

    }
}
