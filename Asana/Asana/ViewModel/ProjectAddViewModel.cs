using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using Asana.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class ProjectAddViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly IProjectService projectService;
        private readonly NavigationService navigation;
        public ProjectAddViewModel(NavigationService navigation)
        {
            ProjectManager = CurrentUser.Instance.User.FullName;
            projectService = new ProjectService();
            Deadline = DateTime.Now;
            this.navigation = navigation;
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
        private RelayCommand createCommand;
        public RelayCommand CreateCommand => createCommand ?? (createCommand = new RelayCommand(
        () =>
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                //Project project = new Project
                //{
                //    Name = ProjectName,
                //    ProjectEmail = ProjectEmail,
                //    UserId = CurrentUser.Instance.User.Id,
                //    Description = Description,
                //    ProjectManager = ProjectManager
                //};
                //projectService.CreateAsync(project);

                using (var db = new AsanaDbContext())
                {
                    CurrentProject.Instance.Project = db.Projects
                                                        .Include("User")
                                                        .Include("Users")
                                                        .Include("Columns")
                                                        .First(x=>x.UserId==CurrentUser.Instance.User.Id);

                }
                navigation.NavigateTo(ViewType.ProjectPage);
                Closewindow();
            });

        }));

        private RelayCommand closeWindowCommand;
        public RelayCommand CloseWindowCommand => closeWindowCommand ?? (closeWindowCommand = new RelayCommand(
        () =>
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Closewindow();
            });

        }));


        private string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set { Set(ref projectName, value); }
        }

        private string projectEmail;
        public string ProjectEmail
        {
            get { return projectEmail; }
            set { Set(ref projectEmail, value); }
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

        private DateTime deadline;
        public DateTime Deadline
        {
            get { return deadline; }
            set { Set(ref deadline, value); }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;

                if (columnName.Equals(nameof(ProjectEmail)))
                {
                    if (String.IsNullOrWhiteSpace(ProjectEmail) || !RegexChecker.CheckEmail(ProjectEmail))
                    {
                        result = "You must enter email for project with correct form.";
                    }
                }
                else if (columnName.Equals(nameof(ProjectName)))
                {
                    if (String.IsNullOrWhiteSpace(ProjectName))
                    {
                        result = "You must enter title for project!";
                    }
                }
                return result;
            }

        }
    }
}
