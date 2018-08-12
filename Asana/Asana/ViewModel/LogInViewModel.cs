using Asana.Navigation;
using Asana.Objects;
using Asana.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Asana.ViewModel
{
    public class LogInViewModel:ViewModelBase
    {
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



        //private RelayCommand _checkPassCommand;

        //public RelayCommand CheckPassCommand => _checkPassCommand ?? (_checkPassCommand = new RelayCommand(
        //    x =>
        //    {
               
        //    }
        //    ));
      

        private RelayCommand _logInBtnCommand;

        public RelayCommand LogInBtnCommand => _logInBtnCommand ?? (_logInBtnCommand = new RelayCommand(
                   x =>
                   {
                       using (var db = new AsanaDbContext())
                       {
                           db.ExtraInfos.Any(user => user.Email == Email && user.Password == Password);
                       }
                   }));



        private RelayCommand _forgotPassCommand;

        public RelayCommand ForgotPassCommand => _forgotPassCommand ?? (_forgotPassCommand = new RelayCommand(
               x => navigation.NavigateTo(ViewType.ForgetPass)

            ));

        private RelayCommand _signUpCommand;

        public RelayCommand SignUpCommand => _signUpCommand ?? (_signUpCommand = new RelayCommand(
            x =>navigation.NavigateTo(ViewType.SignUp) 
            ));

    }
}
