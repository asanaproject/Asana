using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asana.Navigation;
using Asana.Tools;

namespace Asana.ViewModel
{
    public class ForgetPassViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;
        public ForgetPassViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }

        private RelayCommand _cancelCommand;

        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            x =>navigation.NavigateTo(ViewType.LogIn)
            ));


    }
}
