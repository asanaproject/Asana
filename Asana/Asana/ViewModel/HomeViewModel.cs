using Asana.Navigation;
using Asana.Tools;
using GalaSoft.MvvmLight;
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

        private RelayCommand _nextSlide;

        public RelayCommand NextSlide => _nextSlide ?? (_nextSlide = new RelayCommand(
        x =>
        {
        }));


        private RelayCommand _backSlide;

        public RelayCommand BackSlide => _backSlide ?? (_backSlide = new RelayCommand(
        x =>
        {
        }));

    }
}
