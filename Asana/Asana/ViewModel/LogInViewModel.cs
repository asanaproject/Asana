using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
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

        private readonly NavigationService navigation;
        public LogInViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            accountService = new AccountService();
        }


        private string email;

        public string Email
        {
            get { return email; }
            set { Set(ref email, value); }
        }

        private string pass ;

        public string Password
        {
            get { return pass; }
            set { Set(ref pass, value); }
        }


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

        private RelayCommand _logInBtnCommand;

        public RelayCommand LogInBtnCommand => _logInBtnCommand ?? (_logInBtnCommand = new RelayCommand(
                   () =>
                   {
                       ExtraWindow extraWindow = new ExtraWindow(new LodingViewModel(), 200, 200);
                       System.Threading.Tasks.Task.Run(() =>
                       {

                           if (accountService.LoginControl(Email, Password))
                           {
                               CloseWindow();
                               CheckLoginLog.Save(Email);
                               navigation.NavigateTo(ViewType.Home);
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

        private RelayCommand _forgotPassCommand;
        public RelayCommand ForgotPassCommand
        {
            get => _forgotPassCommand ?? (_forgotPassCommand = new RelayCommand(
                (() => navigation.NavigateTo(ViewType.ForgotEmailCode)
                )));
        }


        private RelayCommand goToLogInView;
        public RelayCommand GoToLogInView
        {
            get => goToLogInView ?? (goToLogInView = new RelayCommand(
                (() => navigation.NavigateTo(ViewType.RegisterEmail)
                )));
        }

    }
}
