using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using Asana.Tools;
using Asana.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Asana.ViewModel
{
    public class EditTaskViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;
        private readonly ITaskService taskService;
        private readonly ITaskLogService taskLogService;
        private readonly IColumnService columnService;
        private readonly IUserRoleService userRoleService;
        private System.Timers.Timer timer;

        public EditTaskViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            taskService = new TaskService();
            userRoleService = new UserRoleService();
            columnService = new ColumnService();
            taskLogService = new TaskLogService();

            taskService.GetKanbanStatesOfTask();
            userRoleService.LoadRoles(CurrentProject.Instance.Project.Id);


            Title = CurrentTask.Instance.Task.Title;
            ProjectTitle = CurrentProject.Instance.Project.Name;
            SelectedEmployee = CurrentUserRoles.Instance.Employees.FirstOrDefault(x => x.FullName.Equals(CurrentTask.Instance.Task.AssignedTo));
            Deadline = CurrentTask.Instance.Task.Deadline;
            CurrentKanbanState = CurrentProject.Instance.KanbanStates.FirstOrDefault(x => x.Name.Equals(CurrentTask.Instance.Task.CurrentKanbanState));

            Description = CurrentTask.Instance.Task.Description;

            if (CurrentTask.Instance.Task.Image == null)
            {
                TaskImgPath = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory + "\\empty_task_img.png")));
            }
            else
            {
                TaskImgPath = ProfilePhoto.ByteArrayToImage(CurrentTask.Instance.Task.Image);
            }
            userRoleService.LoadRoles(CurrentProject.Instance.Project.Id);
            SelectedEmployee = CurrentUserRoles.Instance.Employees.FirstOrDefault(x => x.FullName.Equals(CurrentTask.Instance.Task.AssignedTo));
            CreatedAt = "Created at: " + CurrentTask.Instance.Task.CreatedAt.Humanize();

            timer = new System.Timers.Timer(1000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;

        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CreatedAt = "Created at: " + CurrentTask.Instance.Task.CreatedAt.Humanize();

        }
        private string createdAt;
        public string CreatedAt
        {
            get { return createdAt; }
            set { Set(ref createdAt, value); }
        }



        private string description;
        public string Description
        {
            get { return description; }
            set { Set(ref description, value); }
        }


        private UserRoles selectedEmployee;
        public UserRoles SelectedEmployee
        {
            get { return selectedEmployee; }
            set { Set(ref selectedEmployee, value); }
        }


        private KanbanState currentKanbanState;
        public KanbanState CurrentKanbanState
        {
            get { return currentKanbanState; }
            set { Set(ref currentKanbanState, value); }
        }


        private DateTime? deadline;
        public DateTime? Deadline
        {
            get { return deadline; }
            set { Set(ref deadline, value); }
        }
        public void Closewindow()
        {

            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title.Equals("ExtraWindow"))
                        window.Close();
                }
            });
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }


        private BitmapImage taskImgPath;
        public BitmapImage TaskImgPath
        {
            get { return taskImgPath; }
            set { Set(ref taskImgPath, value); }
        }

        private string projectTitle;
        public string ProjectTitle
        {
            get { return projectTitle; }
            set { Set(ref projectTitle, value); }
        }

        /// <summary>
        /// Command closes window which is for editing task
        /// </summary>
        private RelayCommand _closeWindowCommand;
        public RelayCommand CloseWindowCommand => _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(
        () =>
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Closewindow();
            });

        }));

        private RelayCommand<UserRoles> employeeChangedCommand;
        public RelayCommand<UserRoles> EmployeeChangedCommand => employeeChangedCommand ?? (employeeChangedCommand = new RelayCommand<UserRoles>(
        x =>
        {
            if (x != null)
            {
                SelectedEmployee = x;
            }
        }));


       


        private RelayCommand<KanbanState> kanbanStateChangedCommand;
        public RelayCommand<KanbanState> KanbanStateChangedCommand => kanbanStateChangedCommand ?? (kanbanStateChangedCommand = new RelayCommand<KanbanState>(
        x =>
        {
            if (x != null)
            {

                CurrentKanbanState = x;
            }
        }));
        private RelayCommand _loadImageCommand;
        public RelayCommand LoadImageCommand => _loadImageCommand ?? (_loadImageCommand = new RelayCommand(
            () =>
            {
                var path = ProfilePhoto.LoadImage();
                if (File.Exists(path))
                {
                    TaskImgPath = new BitmapImage(new Uri(path));
                }
            }
            ));

        /// <summary>
        /// submits changes about task
        /// </summary>
        private RelayCommand submitCommand;
        public RelayCommand SubmitCommand => submitCommand ?? (submitCommand = new RelayCommand(
        () =>
        {

            if (CurrentProject.Instance.Project.ProjectManager.Equals(CurrentUser.Instance.User.FullName) ||
                    CurrentTask.Instance.Task.AssignedTo.Equals(CurrentUser.Instance.User.FullName))
            {
                var lastTask = taskService.FindById(CurrentTask.Instance.Task.Id);

                CurrentTask.Instance.Task.Image = ProfilePhoto.ImageToByteArray(
                       ProfilePhoto.BitmapImageToBitmap(TaskImgPath));
                CurrentTask.Instance.Task.Title = Title;
                CurrentTask.Instance.Task.AssignedTo = SelectedEmployee.FullName;
                CurrentTask.Instance.Task.Deadline = Deadline;
                CurrentTask.Instance.Task.Description = Description;
                CurrentTask.Instance.Task.CurrentKanbanState = CurrentKanbanState.Name;

                taskService.UpdateAsync(CurrentTask.Instance.Task);


                var log = new TaskLog
                {
                    TaskId = lastTask.Id,
                    ChangedBy = CurrentUser.Instance.User.FullName
                };

                if (!CurrentTask.Instance.Task.Title.Equals(lastTask.Title))
                {
                    log.Message += TaskLogMessages.TitleChangeMessage(lastTask.Title, CurrentTask.Instance.Task.Title);
                }
                if (lastTask.CurrentKanbanState != null &&
                    !CurrentTask.Instance.Task.CurrentKanbanState.Equals(lastTask.CurrentKanbanState))
                {
                    log.Message = TaskLogMessages.KanbanStateChangedMessage(lastTask.CurrentKanbanState, CurrentTask.Instance.Task.CurrentKanbanState);

                }
                else
                {
                    log.Message = TaskLogMessages.KanbanStateChangedMessage("", CurrentTask.Instance.Task.CurrentKanbanState);
                }

                if (CurrentTask.Instance.Task.Deadline != (lastTask.Deadline))
                {
                    log.Message += TaskLogMessages.DeadlineChangedMessage(lastTask.Deadline, CurrentTask.Instance.Task.Deadline);
                }

                if (CurrentTask.Instance.Task.Description != (lastTask.Description))
                {
                    log.Message += TaskLogMessages.DescriptionChangedMessage(lastTask.Description, CurrentTask.Instance.Task.Description);
                }
                taskLogService.CreateAsync(log);
                columnService.LoadColumns(CurrentProject.Instance.Project.Id);
                columnService.LoadColumns(CurrentProject.Instance.Project.Id);
                columnService.LoadColumns(CurrentProject.Instance.Project.Id);
                columnService.LoadColumns(CurrentProject.Instance.Project.Id);
                Closewindow();
            }
            else
            {
                Closewindow();
                MessageBox.Show($"You aren't permitted to changed the information about the task: {CurrentTask.Instance.Task.Title}",
                  "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }



        }));
    }
}