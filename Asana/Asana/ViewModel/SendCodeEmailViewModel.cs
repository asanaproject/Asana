using Asana.Model;
using Asana.Navigation;
using Asana.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
     public class SendCodeEmailViewModel :ViewModelBase
    {
        EmailHelper emailHelper = new EmailHelper();
        private readonly NavigationService navigation;

        public SendCodeEmailViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; Set(ref email, value); }
        }

        private RelayCommand _cancelCommand;

        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            () => navigation.NavigateTo(ViewType.LogIn)
            ));


        private RelayCommand _sendCommand;

        public RelayCommand SendCommand => _sendCommand ?? (_sendCommand = new RelayCommand(
            () =>
            {

                if (RegexChecker.CheckEmail(Email))
                {
                    Task.Run(() =>
                    {
                        emailHelper.SendForgotPasswordCode(Email);
                    });
                   CurrentUser.Instance.User.Email=Email;
                    navigation.NavigateTo(ViewType.ConfirmCode);
                }
                else
                {
                    Errors.SendCodeErrorMsg();
                    return;
                }
            }
        ));
    }
}
