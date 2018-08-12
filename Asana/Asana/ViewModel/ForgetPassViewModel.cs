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
        EmailHelper emailHelper = new EmailHelper();
        private readonly NavigationService navigation;
        public ForgetPassViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; Set(ref _email, value); }
        }

        private RelayCommand _emailCheckCommand;

        public RelayCommand EmailCheckCommand => _emailCheckCommand ?? (_emailCheckCommand = new RelayCommand(
            x =>
            {
                emailHelper.SendForgotPasswordCode(Email);

            } ));


        private RelayCommand _cancelCommand;

        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            x =>navigation.NavigateTo(ViewType.LogIn)
            ));


    }
}
