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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;
        private readonly IProjectService projectService;

        public HomeViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            projectService = new ProjectService();

            timer = new System.Timers.Timer(2000);
            timer.Elapsed += Timer_Elapsed;
        }
        

        /// <summary>
        /// command changes the view to chatpage which is designed for employees of project
        /// </summary>
        private RelayCommand _discussCommand;
        public RelayCommand DiscussCommand
        {
            get => _discussCommand ?? (_discussCommand = new RelayCommand(
                (() =>
                {
                    timer.Stop();
                    navigation.NavigateTo(ViewType.ChatView);
                }
                )));
        }

        /// <summary>
        /// command changes the view to list of projects which current user are created or part of them
        /// </summary>
        private RelayCommand _projectCommand;
        public RelayCommand ProjectCommand
        {

            get => _projectCommand ?? (_projectCommand = new RelayCommand(
                (() =>
                {
                    projectService.LoadProjects(CurrentUser.Instance.User.Id);
                    navigation.NavigateTo(ViewType.ProjectPage);
                }
                )));
        }

        
        /// <summary>
        /// command changes the view to setting page 
        /// </summary>
        private RelayCommand _settingsCommand;
        public RelayCommand SettingsCommand
        {
            get => _settingsCommand ?? (_settingsCommand = new RelayCommand(
                (() => navigation.NavigateTo(ViewType.Home)
                )));
        }


        private RelayCommand _appsCommand;
        public RelayCommand AppsCommand
        {
            get => _appsCommand ?? (_appsCommand = new RelayCommand(
                (() => navigation.NavigateTo(ViewType.Home)
                )));
        }

        private int selectedColumn = 1;

        public int SelectedColumn
        {
            get { return selectedColumn; }
            set { Set(ref selectedColumn, value); }
        }


        private RelayCommand _nextSlide;

        public RelayCommand NextSlide => _nextSlide ?? (_nextSlide = new RelayCommand(
        () =>
        {
            if (SelectedColumn + 1 < 5)
                SelectedColumn++;
        }));

        private RelayCommand _backSlide;

        public RelayCommand BackSlide => _backSlide ?? (_backSlide = new RelayCommand(
        () =>
        {
            if (SelectedColumn - 1 > 0)
                SelectedColumn--;
        }));

        System.Timers.Timer timer;


        private RelayCommand _loadedCommand;

        public RelayCommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new RelayCommand((() =>
            {
                timer.Start();
            }
            )));
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SelectedColumn++;
            if (SelectedColumn == 5)
                SelectedColumn = 1;
        }

        /// <summary>
        /// command closes the project
        /// </summary>
        private RelayCommand _closedCommand;
        public RelayCommand ClosedCommand
        {
            get => _closedCommand ?? (_closedCommand = new RelayCommand((() => { SelectedColumn = 0; timer.Stop(); })));
        }

    }
}
