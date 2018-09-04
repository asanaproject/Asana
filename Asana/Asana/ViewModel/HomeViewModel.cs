using Asana.Navigation;
using Asana.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;


        public HomeViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }

        private RelayCommand _discussCommand;
        public RelayCommand DiscussCommand
        {
            get => _discussCommand ?? (_discussCommand = new RelayCommand(
                (()=> navigation.NavigateTo(ViewType.ChatView)
                )));
        }


        private RelayCommand _projectCommand;
        public RelayCommand ProjectCommand
        {
            get => _projectCommand ?? (_projectCommand = new RelayCommand(
                (()=> navigation.NavigateTo(ViewType.CreateProject)
                )));
        }


        private RelayCommand _settingsCommand;
        public RelayCommand SettingsCommand
        {
            get => _settingsCommand ?? (_settingsCommand = new RelayCommand(
                (()=> navigation.NavigateTo(ViewType.Home)
                )));
        }


        private RelayCommand _appsCommand;
        public RelayCommand AppsCommand
        {
            get => _appsCommand ?? (_appsCommand = new RelayCommand(
                (()=> navigation.NavigateTo(ViewType.Home)
                )));
        }



        private RelayCommand _nextSlide;

        public RelayCommand NextSlide => _nextSlide ?? (_nextSlide = new RelayCommand(
        ()=>
        {
        }));

        private RelayCommand _backSlide;

        public RelayCommand BackSlide => _backSlide ?? (_backSlide = new RelayCommand(
        () =>
        {
        }));
    }
}
