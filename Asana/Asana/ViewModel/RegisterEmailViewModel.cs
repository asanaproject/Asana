using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Tools;
using Asana.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    /// <summary>
    /// This class is for registering user's email and sending confirmation code to this email
    /// </summary>
    public class RegisterEmailViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly NavigationService navigation;
        EmailHelper GetEmail = new EmailHelper();
        public RegisterEmailViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            Email = String.Empty;
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                Set(ref email, value);
            }
        }

        /// <summary>
        /// validation for email
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName.Equals(nameof(Email)))
                {
                    if (!RegexChecker.CheckEmail(Email))
                        result = "Enter your email correctly!";
                    else if (RegexChecker.CheckEmail(Email))
                    {
                        using (var db = new AsanaDbContext())
                        {
                            if (db.Users.ToList().Exists(user => user.Email == Email))
                            {
                                result = "This mail already exists!!!";
                            }
                        }
                    }
                }


                return result;
            }
        }
        public string Error => throw new NotImplementedException();


        /// <summary>
        /// method closes the loading window
        /// </summary>
        public void CloseWindow()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title == "ExtraWindow")
                        window.Close();
                }
                WindowBluringCustom.Normal();
            });
        }


        /// <summary>
        /// when sign up button is clicked current view is replaced with ConfirmationCode view 
        /// and code will be sent to your email 
        /// </summary>
        private RelayCommand sendConfirmationCodeCommand;
        public RelayCommand SendConfirmationCodeCommand => sendConfirmationCodeCommand ?? (sendConfirmationCodeCommand = new RelayCommand(
                () =>
                {
                    ExtraWindow extraWindow = new ExtraWindow(new LodingViewModel(), 200, 200);
                    extraWindow.ShowInTaskbar = false;

                    System.Threading.Tasks.Task.Run(
                        () =>
                        {

                            if (RegexChecker.CheckEmail(Email))
                            {

                                GetEmail.SendRegisterActivationCode(Email);
                                CloseWindow();
                                navigation.NavigateTo(ViewType.ConfirmCode);
                                MessageBox.Show($"Confirmation code is sent to {Email}, please, check your email and enter it to box.", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
                                CurrentUser.Instance.User = new User();
                                CurrentUser.Id = CurrentUser.Instance.User.Id;
                                CurrentUser.Instance.User.Email = Email;
                                Email = String.Empty;
                            }
                            else Errors.SendCodeErrorMsg();
                            CloseWindow();
                        });
                    WindowBluringCustom.Bluring();
                    extraWindow.ShowDialog();
                    WindowBluringCustom.Normal();

                }
            ));


        /// <summary>
        /// When arrow key is pressed current view is replaced with LogIn view, email that is entered user will be resetted
        /// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            () =>
            {
                ExtraWindow extraWindow = new ExtraWindow(new LodingViewModel(), 200, 200);
                extraWindow.ShowInTaskbar = false;

                System.Threading.Tasks.Task.Run(() =>
              {
                  Email = String.Empty;
                  navigation.NavigateTo(ViewType.LogIn);
                  CloseWindow();

              });
                WindowBluringCustom.Bluring();
                extraWindow.ShowDialog();
                WindowBluringCustom.Normal();
            }
            ));
    }
}
