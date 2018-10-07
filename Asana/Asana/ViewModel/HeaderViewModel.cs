using Asana.Model;
using Asana.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Asana.ViewModel
{
    public class HeaderViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;
        private readonly AccountService accountService;

        private string header;

        public string Header
        {
            get { return header; }
            set { Set(ref header, value); }
        }

        private string background;

        public string Background
        {
            get { return background; }
            set { Set(ref background, value); }
        }

        private string menuForeground;

        public string MenuForeground
        {
            get { return menuForeground; }
            set { Set(ref menuForeground, value); }
        }

        public void CustomHeader()
        {
            Background = "#FFFFFF";
            MenuForeground = "#8f8f8f";
            ViewVisible = Visibility.Collapsed;
        }

        public void DefaultHeader()
        {
            Background = "#FF9D79B0";
            MenuForeground = "#f5f5f5";
            ViewVisible = Visibility.Visible;
        }


        public HeaderViewModel(NavigationService navigationService, string header = "Project", HeaderType headerType = HeaderType.DefaultType)
        {
            this.navigationService = navigationService;
            accountService = new AccountService();
            Header = header;

            switch (headerType)
            {
                case HeaderType.CreateProject:
                    CustomHeader();
                    break;
                case HeaderType.DefaultType:
                    DefaultHeader();
                    break;
            }

        }

        private RelayCommand _homeCommand;
        public RelayCommand HomeCommand
        {
            get => _homeCommand ?? (_homeCommand = new RelayCommand(
                (() => navigationService.NavigateTo(ViewType.Home)
                )));
        }


        private RelayCommand _dashboardCommand;
        public RelayCommand DashboardCommand
        {
            get => _dashboardCommand ?? (_dashboardCommand = new RelayCommand(
                (() => navigationService.NavigateTo(ViewType.Home)
                )));
        }

        private RelayCommand _projectCommand;
        public RelayCommand ProjectCommand
        {
            get => _projectCommand ?? (_projectCommand = new RelayCommand(
                (() => navigationService.NavigateTo(ViewType.CreateProject)
                )));
        }


        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get => _searchCommand ?? (_searchCommand = new RelayCommand(
                (() => navigationService.NavigateTo(ViewType.Home)
                )));
        }


        private RelayCommand _settingsCommand;
        public RelayCommand SettingsCommand
        {
            get => _settingsCommand ?? (_settingsCommand = new RelayCommand(
                (() => navigationService.NavigateTo(ViewType.Home)
                )));
        }

        private RelayCommand _profileCommand;
        public RelayCommand ProfileCommand
        {
            get => _profileCommand ?? (_profileCommand = new RelayCommand(
                (() => navigationService.NavigateTo(ViewType.Profile)
                )));
        }

        private RelayCommand _logoutCommand;
        public RelayCommand LogoutCommand
        {
            get => _logoutCommand ?? (_logoutCommand = new RelayCommand(
                () =>
                {

                    System.Threading.Tasks.Task.Run(() =>
                    {
                        accountService.Logout();
                        navigationService.NavigateTo(ViewType.LogIn);
                    });

                }));
        }


        private Visibility viewVisible;

        public Visibility ViewVisible
        {
            get { return viewVisible; }
            set { Set(ref viewVisible, value); }
        }


    }
}
