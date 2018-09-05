using Asana.Model;
using Asana.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.ViewModel
{
    public class HeaderViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;
        private readonly AccountService accountService;
        public HeaderViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            accountService = new AccountService();
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

        private RelayCommand _logoutCommand;
        public RelayCommand LogoutCommand
        {
            get => _logoutCommand ?? (_logoutCommand = new RelayCommand(
                (() =>
                {
                    accountService.Logout();
                    navigationService.NavigateTo(ViewType.LogIn);
                }
                )));
        }


    }
}
