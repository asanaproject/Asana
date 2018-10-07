using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using Asana.Tools;
using Asana.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Asana.ViewModel
{
    public class LogInViewModel : ViewModelBase
    {
        public readonly AccountService accountService;
        public readonly UserService userService;
        public readonly IProjectService projectService;

        private readonly NavigationService navigation;
        public LogInViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            accountService = new AccountService();
            projectService = new ProjectService();
        }


        private string email;
        public string Email
        {
            get { return email; }
            set { Set(ref email, value); }
        }

        private string pass;
        public string Password
        {
            get { return pass; }
            set { Set(ref pass, value); }
        }

        /// <summary>
        /// command closes the loading window
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
        /// if email and password of user are entered correctly and user already registered, command leads the user to List of projects page
        /// </summary>
        private RelayCommand _logInBtnCommand;
        public RelayCommand LogInBtnCommand => _logInBtnCommand ?? (_logInBtnCommand = new RelayCommand(
                   () =>
                   {
                       ExtraWindow extraWindow = new ExtraWindow(new LodingViewModel(), 200, 200);
                       extraWindow.ShowInTaskbar = false;
                       System.Threading.Tasks.Task.Run(() =>
                       {

                           if (accountService.LoginControl(Email, Password))
                           {
                               CloseWindow();
                               CheckLoginLog.Save(Email);
                               projectService.LoadProjects(CurrentUser.Instance.User.Id);
                               Email = String.Empty;
                               Password = String.Empty;
                               navigation.NavigateTo(ViewType.CreateProject);
                           }
                           else
                               Errors.LoginErrorMsg();
                           CloseWindow();
                       }
                       );
                       WindowBluringCustom.Bluring();
                       extraWindow.ShowDialog();
                       WindowBluringCustom.Normal();
                   }));

        /// <summary>
        /// command leads you to the page for resetting the your password 
        /// </summary>
        private RelayCommand _forgotPassCommand;
        public RelayCommand ForgotPassCommand
        {
            get => _forgotPassCommand ?? (_forgotPassCommand = new RelayCommand(
                (() =>
                {
                    Email = String.Empty;
                    Password = String.Empty;
                    navigation.NavigateTo(ViewType.ForgotEmailCode);
                }
                )));
        }

        /// <summary>
        /// command leads you to Registration page
        /// </summary>
        private RelayCommand goToLogInView;
        public RelayCommand GoToLogInView
        {
            get => goToLogInView ?? (goToLogInView = new RelayCommand(
                (() =>
                {
                    Email = String.Empty;
                    Password = String.Empty;
                    navigation.NavigateTo(ViewType.RegisterEmail);
                }
                )));
        }

    }
}
