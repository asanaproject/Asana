using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Asana.ViewModel
{
    public class LogInViewModel:ViewModelBase
    {
        public readonly AccountService accountService;
        private readonly NavigationService navigation;
        public LogInViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; Set(ref email, value); }
        }

        private string pass;

        public string Password
        {
            get { return pass; }
            set { pass = value; Set(ref pass, value); }
        }

      

        private RelayCommand _logInBtnCommand;

        public RelayCommand LogInBtnCommand => _logInBtnCommand ?? (_logInBtnCommand = new RelayCommand(
                   x =>
                   {
                       if (accountService.LoginControl(Email, Password))
                           navigation.NavigateTo(ViewType.Home);
                       else
                           Errors.LoginErrorMsg();
                   }));

        private RelayCommand _forgotPassCommand;
        public RelayCommand ForgotPassCommand
        {
            get => _forgotPassCommand ?? (_forgotPassCommand = new RelayCommand(
                (x => navigation.NavigateTo(ViewType.CreateProject)
                )));
        }


        private RelayCommand goToLogInView;
        public RelayCommand GoToLogInView
        {
            get => goToLogInView ?? (goToLogInView = new RelayCommand(
                (x => navigation.NavigateTo(ViewType.RegisterEmail)
                )));
        }

    }
}
