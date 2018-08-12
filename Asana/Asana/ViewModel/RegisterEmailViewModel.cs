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
        public RegisterEmailViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            User = new User();
            showGrid = Visibility.Hidden;
            signUpButton = Visibility.Visible;
            confirmButton = Visibility.Hidden;
        }
        EmailHelper GetEmail = new EmailHelper();
        private Visibility showGrid;
        public Visibility ShowGrid
        {
            get { return showGrid; }
            set { Set(ref showGrid, value); }
        }

        private Visibility signUpButton;
        public Visibility SignUpButton
        {
            get { return signUpButton; }
            set { Set(ref signUpButton, value); }
        }
        private Visibility confirmButton;
        public Visibility ConfirmButton
        {
            get { return confirmButton; }
            set { Set(ref confirmButton, value); }
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
        private string confirmationCode;
        public string ConfirmationCode
        {
            get { return confirmationCode; }
            set
            {
                Set(ref confirmationCode, value);
            }
        }

        private RelayCommand sendConfirmationCode;
        public RelayCommand SendConfirmationCode
        {
            get => sendConfirmationCode ?? (sendConfirmationCode = new RelayCommand(
                x =>
                {
                    ShowGrid = Visibility.Visible;
                    SignUpButton = Visibility.Hidden;
                    ConfirmButton = Visibility.Visible;
                    GetEmail.SendRegisterActivationCode(User.Email);
                    var result = MessageBox.Show($"Confirmation code is sent to {User.Email}, please, check your email and enter it to box.", "Email", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            ));
        }
        private RelayCommand codeConfirmation;
        public RelayCommand CodeConfirmation
        {
            get => codeConfirmation ?? (codeConfirmation = new RelayCommand(
                x =>
                {
                    if (Randomizer.RandomKey.Equals(ConfirmationCode))
                    {
                        navigation.NavigateTo(ViewType.LogIn);
                    }
                }
            ));
        }
    }
}
