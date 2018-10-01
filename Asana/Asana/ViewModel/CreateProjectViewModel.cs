using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using Asana.Tools;
using Asana.View;
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
    public class CreateProjectViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;
        private readonly IProjectService projectService;
        private readonly IColumnService columnService;
        public CreateProjectViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            projectService = new ProjectService();
            columnService = new ColumnService();
            header = new HeaderViewModel(navigation, "Project", HeaderType.CreateProject);
        }
        private object header;
        public object Header
        {
            get { return header; }
            set { Set(ref header, value); }
        }


        private RelayCommand<Project> projectInfoCommand;
        public RelayCommand<Project> ProjectInfoCommand => projectInfoCommand ?? (projectInfoCommand = new RelayCommand<Project>(
        x =>
        {
            CurrentProject.Instance.Project = x;
            WindowBluringCustom.Bluring();
            ExtraWindow extraWindow = new ExtraWindow(new ProjectViewModel(navigation), 700, 410);
            extraWindow.ShowDialog();
            WindowBluringCustom.Normal();

        }));



        private RelayCommand<Project> selectProjectCommand;
        public RelayCommand<Project> SelectProjectCommand => selectProjectCommand ?? (selectProjectCommand = new RelayCommand<Project>(
        x =>
        {
            CurrentProject.Instance.Project = x;
            columnService.LoadColumns(CurrentProject.Instance.Project.Id);
            navigation.NavigateTo(ViewType.ProjectPage);

        }));


        private RelayCommand<Project> editProjectCommand;
        public RelayCommand<Project> EditProjectCommand => editProjectCommand ?? (editProjectCommand = new RelayCommand<Project>(
        x =>
        {
            projectService.UpdateAsync(x);
            var projects = projectService.GetAll(CurrentUser.Instance.User.Id);
            if (projects != null)
            {
                ProjectsOfUser.Instance.Projects = projects as ObservableCollection<Project>;
            }
        }));


        private RelayCommand<Project> deleteProjectCommand;
        public RelayCommand<Project> DeleteProjectCommand => deleteProjectCommand ?? (deleteProjectCommand = new RelayCommand<Project>(
        x =>
        {
            projectService.RemoveAsync(x);
            projectService.LoadProjects(CurrentUser.Instance.User.Id);
            projectService.LoadProjects(CurrentUser.Instance.User.Id);
        }));


        private RelayCommand createProject;
        public RelayCommand CreateProject => createProject ?? (createProject = new RelayCommand(
            () =>
            {
                WindowBluringCustom.Bluring();
                ExtraWindow extraWindow = new ExtraWindow(new ProjectAddViewModel(navigation), 450, 300);
                extraWindow.ShowDialog();
                WindowBluringCustom.Normal();
            }
            ));
    }
}
