using Asana.Model;
using Asana.Navigation;
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
    public class ProjectViewModel:ViewModelBase
    {
        private readonly NavigationService navigation;
        public ProjectViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            ProjectTitle = CurrentProject.Instance.Project.Name;
            ProjectEmail = CurrentProject.Instance.Project.ProjectEmail;
            ProjectManager = CurrentProject.Instance.Project.ProjectManager;
            var columns = CurrentProject.Instance.Project.Columns.Count();
            CountOfColumns = (columns == 0) ? "0" : columns.ToString();
            int count = 0;
            CurrentProject.Instance.Project.Columns.ToList().ForEach(z=> count++);
            CountOfTasks =(count==0)?"0": count.ToString();
            Description = CurrentProject.Instance.Project.Description;

            timer = new System.Timers.Timer(1000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;

        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CreatedAt = "Created at: " + CurrentProject.Instance.Project.CreatedAt.Humanize();

        }
        System.Timers.Timer timer;

        private string description;
        public string Description
        {
            get { return description; }
            set { Set(ref description, value); }
        }


        private string createdAt;
        public string CreatedAt
        {
            get { return createdAt; }
            set { Set(ref createdAt, value); }
        }

        private string deadline;
        public string Deadline
        {
            get { return deadline; }
            set { Set(ref deadline, value); }
        }

        private string projectTitle;
        public string ProjectTitle
        {
            get { return projectTitle; }
            set {Set(ref projectTitle,value); }
        }


        private string projectManager;
        public string ProjectManager
        {
            get { return projectManager; }
            set { Set(ref projectManager, value); }
        }

        private string countOfColumns;
        public string CountOfColumns
        {
            get { return countOfColumns; }
            set { Set(ref countOfColumns, value); }
        }

        private string countOfTasks;
        public string CountOfTasks
        {
            get { return countOfTasks; }
            set { Set(ref countOfTasks, value); }
        }

        private string projectEmail;
        public string ProjectEmail
        {
            get { return projectEmail; }
            set { Set(ref projectEmail, value); }
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
        private RelayCommand closeWindowCommand;
        public RelayCommand CloseWindowCommand => closeWindowCommand ?? (closeWindowCommand = new RelayCommand(
        () =>
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Closewindow();
            });

        }));

    }
}
