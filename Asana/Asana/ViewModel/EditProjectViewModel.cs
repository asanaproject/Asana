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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class EditProjectViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;
        private System.Timers.Timer timer;
        private readonly IProjectService projectService;
        private readonly IUserRoleService userRoleService;

        public EditProjectViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            projectService = new ProjectService();
            userRoleService = new UserRoleService();

            userRoleService.LoadRoles(CurrentProject.Instance.Project.Id);

            ProjectTitle = CurrentProject.Instance.Project.Name;
            Deadline = CurrentProject.Instance.Project.Deadline;
            SelectedProjectManager = CurrentUserRoles.Instance.Employees.FirstOrDefault(x => x.FullName.Equals(CurrentProject.Instance.Project.ProjectManager));
            ProjectEmail = CurrentProject.Instance.Project.ProjectEmail;
            Description = CurrentProject.Instance.Project.Description;
            CreatedAt = "Created at: " + CurrentProject.Instance.Project.CreatedAt.Humanize();

            timer = new System.Timers.Timer(1000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CreatedAt = "Created at: " + CurrentProject.Instance.Project.CreatedAt.Humanize();
        }
        private string projectTitle;
        public string ProjectTitle
        {
            get { return projectTitle; }
            set { Set(ref projectTitle, value); }
        }

        private string createdAt;
        public string CreatedAt
        {
            get { return createdAt; }
            set { Set(ref createdAt, value); }
        }

        private string projectEmail;
        public string ProjectEmail
        {
            get { return projectEmail; }
            set { Set(ref projectEmail, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { Set(ref description, value); }
        }

        private DateTime? deadline;
        public DateTime? Deadline
        {
            get { return deadline; }
            set { Set(ref deadline, value); }
        }


        private UserRoles selectedProjectManager;
        public UserRoles SelectedProjectManager
        {
            get { return selectedProjectManager; }
            set { Set(ref selectedProjectManager, value); }
        }
        public void Closewindow()
        {

            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title.Equals("ExtraWindow"))
                    {
                        timer.Stop();
                        window.Close();
                    }
                }
            });
        }

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



        private RelayCommand<UserRoles> selectionChangedCommand;
        public RelayCommand<UserRoles> SelectionChangedCommand => selectionChangedCommand ?? (selectionChangedCommand = new RelayCommand<UserRoles>(
        x =>
        {
            if (x != null)
            {
                SelectedProjectManager = x;
            }
        }));

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand => updateCommand ?? (updateCommand = new RelayCommand(
        () =>
        {
            if (CurrentProject.Instance.Project.ProjectManager.Equals(CurrentUser.Instance.User.FullName))
            {

                System.Threading.Tasks.Task.Run(() =>
                {
                    CurrentProject.Instance.Project.Description = Description;
                    CurrentProject.Instance.Project.Deadline = Deadline;
                    CurrentProject.Instance.Project.Name = ProjectTitle;
                    CurrentProject.Instance.Project.ProjectManager = SelectedProjectManager.FullName;
                    CurrentProject.Instance.Project.ProjectEmail = ProjectEmail;

                    projectService.UpdateAsync(CurrentProject.Instance.Project);
                    projectService.LoadProjects(CurrentUser.Instance.User.Id);

                    Closewindow();
                });

            }
            else
            {
                MessageBox.Show($"You aren't permitted to changed the information about the project: {CurrentProject.Instance.Project.Name}",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }));
    }
}
