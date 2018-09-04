using Asana.Model;
using Asana.Navigation;
using Asana.Services.Interfaces;
using Asana.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class SignUpViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly NavigationService navigation;
        private readonly IUserService userService;
        public SignUpViewModel(NavigationService navigation, IUserService userService)
        {
            this.navigation = navigation;
            this.userService = userService;
        }

        private string profieImgPath = "pack://application:,,,/Asana;component/Resources/Images/user.png";
        public string ProfileImgPath
        {
            get { return profieImgPath; }
            set
            {
                Set(ref profieImgPath, value);
            }
        }


        private string fullName = "";
        public string FullName
        {
            get { return fullName; }
            set { Set(ref fullName, value); }
        }

        private string password = "";
        public string Password
        {
            get { return password; }
            set { Set(ref password, value); }
        }


        private string username = "";
        public string UserName
        {
            get { return username; }
            set { Set(ref username, value); }
        }

        private string rePassword = "";
        public string RePassword
        {
            get { return rePassword; }
            set { Set(ref rePassword, value); }
        }
        /// <summary>
        /// Command for come back to login view
        /// </summary>
        private RelayCommand _goToLogInCommand;
        public RelayCommand GoToLogInViewCommand => _goToLogInCommand ?? (_goToLogInCommand = new RelayCommand(
            () => navigation.NavigateTo(ViewType.LogIn)
            ));


        private RelayCommand _loadImageCommand;
        public RelayCommand LoadImageCommand => _loadImageCommand ?? (_loadImageCommand = new RelayCommand(
            () =>
            {
                var path = ProfilePhoto.LoadImage();
                if (!String.IsNullOrEmpty(path))
                {
                    ProfileImgPath = path;
                }
            }
            ));

        private RelayCommand _registerCommand;
        public RelayCommand RegisterCommand => _registerCommand ?? (_registerCommand = new RelayCommand(
            () =>
            {
                try
                {
                    User user = new User() {Email = CurrentUser.Instance.User.Email, FullName = FullName, Image = ProfilePhoto.ImageToByteArray(new Bitmap(ProfileImgPath)), Password = Password, Username = UserName };
                    userService.Insert(user);
                    navigation.NavigateTo(ViewType.LogIn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            ));
        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;
                if (columnName.Equals(nameof(FullName)))
                {
                    if (String.IsNullOrEmpty(FullName))
                    {
                        result = "Add your name, so your teammates\nknow who you are.";
                    }
                }
                else if (columnName.Equals(nameof(UserName)))
                {
                    if (!RegexChecker.CheckUsername(UserName))
                    {
                        result = "Username is not valid";
                    }
                }
                else if (columnName.Equals(nameof(Password)))
                {
                    if (!RegexChecker.CheckPassword(Password))
                    {
                        result = "Set a secure password that's\n8 characters or longer, at least\none upper-case, one lower-case letter.";
                    }
                }
                else if (columnName.Equals(nameof(RePassword)))
                {
                    if (!Password.Equals(RePassword))
                    {
                        result = "Passwords aren't the same.";
                    }
                }
                return result;
            }
        }
    }
}
