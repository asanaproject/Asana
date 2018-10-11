﻿using Asana.Model;
using Asana.Navigation;
using Asana.Services;
using Asana.Services.Interfaces;
using Asana.Tools;
using Asana.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Asana.ViewModel
{
    public class SignUpViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly NavigationService navigation;
        private readonly IUserService userService;
        public SignUpViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            this.userService = new UserService();
            if (CurrentUser.Instance.User.Image == null)
            {
                ProfileImgPath = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory + "\\user.png")));
            }
            else
            {
                ProfileImgPath = ProfilePhoto.ByteArrayToImage(CurrentUser.Instance.User.Image);
            }
        }

        private BitmapImage profieImgPath;
        public BitmapImage ProfileImgPath
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
            () =>
            {
                FullName = String.Empty;
                UserName = String.Empty;
                Password = String.Empty;
                RePassword = String.Empty;
                CurrentUser.Instance.User = new User();

                navigation.NavigateTo(ViewType.LogIn);
            }
            ));


        private RelayCommand _loadImageCommand;
        public RelayCommand LoadImageCommand => _loadImageCommand ?? (_loadImageCommand = new RelayCommand(
            () =>
            {
                var path = ProfilePhoto.LoadImage();
                if (!String.IsNullOrEmpty(path))
                {
                    ProfileImgPath = new BitmapImage(new Uri(path));
                }
            }
            ));

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

        private RelayCommand _registerCommand;
        public RelayCommand RegisterCommand => _registerCommand ?? (_registerCommand = new RelayCommand(
            () =>
            {

                ExtraWindow extraWindow = new ExtraWindow(new LodingViewModel(), 200, 200);
                extraWindow.ShowInTaskbar = false;

                Task.Run(()
                  =>
                {
                    User user = new User();
                    user.Email = CurrentUser.Instance.User.Email;
                    user.FullName = FullName;
                    user.Image = ProfilePhoto.ImageToByteArray(ProfilePhoto.BitmapImageToBitmap(ProfileImgPath));
                    user.Password = Password;
                    user.Username = UserName;
                    userService.CreateAsync(user);
                    FullName = String.Empty;
                    UserName = String.Empty;
                    Password = String.Empty;
                    RePassword = String.Empty;
                    CurrentUser.Instance.User = new User();
                    navigation.NavigateTo(ViewType.LogIn);
                    CloseWindow();

                });
                WindowBluringCustom.Bluring();
                extraWindow.ShowDialog();
                WindowBluringCustom.Normal();
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
