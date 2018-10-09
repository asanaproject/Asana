using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class EditTaskViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;
        private readonly ITaskService taskService;
        System.Timers.Timer timer;
        public EditTaskViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            taskService = new TaskService();
            Title = CurrentColumn.Instance.Column.Title;
            ProjectTitle = CurrentProject.Instance.Project.Name;
            ProjectManager = CurrentProject.Instance.Project.ProjectManager;
            CreatedAt = "Created at: " + CurrentTask.Instance.Task.CreatedAt.Humanize();
            Deadline = CurrentTask.Instance.Task.Deadline;
            SelectedEmployee = CurrentProject.Instance.Project.Users.
                               FirstOrDefault(x=>x.FullName.Equals(CurrentTask.Instance.Task.AssignedTo));
            Description = CurrentTask.Instance.Task.Description;

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
        private string projectManager;

        public string ProjectManager
        {
            get { return projectManager; }
            set { Set(ref projectManager, value); }
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
                timer.Stop();
                Closewindow();
            });

        }));


        /// <summary>
        /// submits changes about task
        /// </summary>
        private RelayCommand submitCommand;
        public RelayCommand SubmitCommand => submitCommand ?? (submitCommand = new RelayCommand(
        () =>
        {
            if (CurrentProject.Instance.Project.ProjectManager.Equals(CurrentUser.Instance.User.Username)||
                CurrentTask.Instance.Task.AssignedTo.Equals(CurrentUser.Instance.User.Username))
            {
                var task = CurrentTask.Instance.Task;
                taskService.UpdateAsync(task);

                System.Threading.Tasks.Task.Run(() =>
                {
                    timer.Stop();
                    Closewindow();
                });
            }
            else
            {
                MessageBox.Show($"You aren't permitted to changed the information about the task: {CurrentTask.Instance.Task.Title}",
                  "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }));
    }
}