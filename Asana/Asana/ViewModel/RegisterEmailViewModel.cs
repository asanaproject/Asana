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
            showGrid = Visibility.Hidden;
            signUpButton = Visibility.Visible;
            confirmButton = Visibility.Hidden;
        }
      
        /// <summary>
        /// property will be visible when email format is correct 
        /// and sign up is clicked, and will be hidden when confirm button is clicked
        /// </summary>
        private Visibility showGrid;
        public Visibility ShowGrid
        {
            get { return showGrid; }
            set { Set(ref showGrid, value); }
        }


        /// <summary>
        /// when sign up button is clicked it will be hidden and confirm button will be visible
        /// </summary>
        private Visibility signUpButton;
        public Visibility SignUpButton
        {
            get { return signUpButton; }
            set { Set(ref signUpButton, value); }
        }

        /// <summary>
        /// when confirm button is clicked it checks the confirmation 
        /// code which is sent to email is the same with code which is inputted to textbox of confirmation code
        /// </summary>
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

        /// <summary>
        /// this property is for textbox which user enters confrimation code which is sent to user's email
        /// </summary>
        private string confirmationCode;
        public string ConfirmationCode
        {
            get { return confirmationCode; }
            set
            {
                Set(ref confirmationCode, value);
            }
        }


        /// <summary>
        /// command sends email to user's email address
        /// </summary>
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


        /// <summary>
        /// command checks sameness of inputted code and code which is sent to user's email
        /// </summary>
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
                    else
                    {
                        MessageBox.Show("Confirmation Code is not correct, enter it correctly!",
                                        "Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
                    }
                }
            ));
        }
    }
}
