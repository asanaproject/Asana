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

namespace Asana.ViewModel
{
    public class CreateProjectViewModel : ViewModelBase, IDropTarget
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

        /// <summary>
        /// command pop's up window which shows informations (deadline, project name, description) about ptoject    
        /// </summary>
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




        /// <summary>
        /// command leads user to project page which shows columns and tasks of project which create by project manager or employees
        /// </summary>
        private RelayCommand<Project> selectProjectCommand;
        public RelayCommand<Project> SelectProjectCommand => selectProjectCommand ?? (selectProjectCommand = new RelayCommand<Project>(
        x =>
        {
            CurrentProject.Instance.Project = x;
           
            columnService.LoadColumns(CurrentProject.Instance.Project.Id);
            navigation.NavigateTo(ViewType.ProjectPage);

        }));


        /// <summary>
        /// command pop's up the window which project manager can edit information about the project like pm, deadline,title, description etc of it
        /// </summary>
        private RelayCommand<Project> editProjectCommand;
        public RelayCommand<Project> EditProjectCommand => editProjectCommand ?? (editProjectCommand = new RelayCommand<Project>(
        x =>
        {
            CurrentProject.Instance.Project = x;

            WindowBluringCustom.Bluring();
            ExtraWindow extraWindow = new ExtraWindow(new EditProjectViewModel(navigation), 750, 370);
            extraWindow.ShowDialog();
            WindowBluringCustom.Normal();

        }));

        /// <summary>
        /// command deletes the project if user is project manager otherwise it pop's up messagebox which says u are not permitted to delete the projct
        /// </summary>
        private RelayCommand<Project> deleteProjectCommand;
        public RelayCommand<Project> DeleteProjectCommand => deleteProjectCommand ?? (deleteProjectCommand = new RelayCommand<Project>(
        x =>
        {
            projectService.RemoveAsync(x);
            projectService.LoadProjects(CurrentUser.Instance.User.Id);
            projectService.LoadProjects(CurrentUser.Instance.User.Id);
            projectService.LoadProjects(CurrentUser.Instance.User.Id);
            projectService.LoadProjects(CurrentUser.Instance.User.Id);
        }));

        /// <summary>
        /// command pop's up the window which requires to fill blanks about project
        /// </summary>
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

        #region Drag/drop for projects
        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            dropInfo.Effects = DragDropEffects.Move | DragDropEffects.Copy;

        }

        public void Drop(IDropInfo dropInfo)
        {

            Project sourceItem = dropInfo.Data as Project;
            Project targetItem = dropInfo.TargetItem as Project;
            if (sourceItem != null && targetItem != null)
            {
                projectService.LoadProjects(CurrentUser.Instance.User.Id);
                var sourceIndex = ProjectsOfUser.Instance.Projects.First(x => x.Id == sourceItem.Id).Position;
                var targetIndex = ProjectsOfUser.Instance.Projects.First(x => x.Id == targetItem.Id).Position;
                if (sourceIndex != targetIndex)
                {
                    projectService.UpdatePositionAsync(sourceIndex, targetItem);
                    projectService.UpdatePositionAsync(targetIndex, sourceItem);
                    projectService.LoadProjects(CurrentUser.Instance.User.Id);
                }
            }
        }
        #endregion


    }
}
