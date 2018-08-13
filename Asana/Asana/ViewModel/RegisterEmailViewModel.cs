using Asana.Navigation;
using Asana.Objects;
using Asana.Tools;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class RegisterEmailViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;
        EmailHelper GetEmail = new EmailHelper();
        public RegisterEmailViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            User = new User();

        }


        private User user;
        public User User
        {
            get { return user; }
            set
            {
                Set(ref user, value);
            }
        }

    


        /// <summary>
        /// when sign up button is clicked view replaced with ConfirmationCode view and code will be sent to your email 
        /// </summary>
        private RelayCommand sendConfirmationCodeCommand;
        public RelayCommand SendConfirmationCodeCommand
        {
            get => sendConfirmationCodeCommand ?? (sendConfirmationCodeCommand = new RelayCommand(
                x =>
                {
                    if (RegexChecker.CheckEmail(User.Email))
                    {
                        GetEmail.SendRegisterActivationCode(User.Email);
                        var result = MessageBox.Show($"Confirmation code is sent to {User.Email}, please, check your email and enter it to box.", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
                        if (result == MessageBoxResult.OK)
                        {
                            navigation.NavigateTo(ViewType.ConfirmCode);
                        }
                    }
                   
                }
            ));
        }


      

        /// <summary>
        /// 
        /// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            x => navigation.NavigateTo(ViewType.LogIn)
            ));
    }
}
