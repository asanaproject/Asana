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
        private readonly IExtraInfoService extraInfoService;
        System.Timers.Timer timer;
        public EditTaskViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            taskService = new TaskService();
            Title = CurrentColumn.Instance.Column.Title;
            extraInfoService = new ExtraInfoService();
            ProjectTitle = CurrentProject.Instance.Project.Name;
            ProjectManager = "Nubar Khalidova";
            CreatedAt = "Created at: " + CurrentTask.Instance.Task.CreatedAt.Humanize();

            timer = new System.Timers.Timer(1000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
            // ProjectManager = CurrentProject.Instance.Project.Users.First(x => x.UserRole.Type.Equals("Project manager")).FullName;
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

        private string email;
        public string Email
        {
            get { return email; }
            set { Set(ref email, value); }
        }

        private string customer;
        public string Customer
        {
            get { return customer; }
            set { Set(ref customer, value); }
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
            var task = CurrentTask.Instance.Task;
            var customer = new ExtraInfo { Email = Email, Username = Customer };
            extraInfoService.Add(customer);
            task.ExtraInfo = customer;
            task.ExtraInfoId = customer.Id;
            taskService.Update(task);
            System.Threading.Tasks.Task.Run(() =>
            {
                timer.Stop();
                Closewindow();
            });
        }));
    }
}