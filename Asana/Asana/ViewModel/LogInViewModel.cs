using Asana.Navigation;
using Asana.Objects;
using Asana.Tools;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
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



        private RelayCommand _logInBtnCommand;

        public RelayCommand LogInBtnCommand => _logInBtnCommand ?? (_logInBtnCommand = new RelayCommand(
                   x =>
                   {
                       using (var db = new AsanaDbContext())
                       {
                           db.ExtraInfos.Any(user => user.Email == Email && user.Password == Password);
                       }
                   }));


       
    }
}
