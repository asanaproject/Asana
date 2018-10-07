using Asana.Model;
using Asana.Navigation;
using Asana.Tools;
using Asana.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class ConfirmCodeViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;

        public ConfirmCodeViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
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
        public static ViewType ViewType { get; set; }

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
        /// command leads user to login page
        /// </summary>
        private RelayCommand _backCommand;
        public RelayCommand BackCommand => _backCommand ?? (_backCommand = new RelayCommand(
            () =>
            {
                ConfirmationCode = String.Empty;
                navigation.NavigateTo(ViewType.LogIn);
            }
            ));

        /// <summary>
        /// command checks sameness of inputted code and code which is sent to user's email
        /// </summary>
        private RelayCommand confirmCommand;
        public RelayCommand ConfirmCommand => confirmCommand ?? (confirmCommand = new RelayCommand(
                () =>
                {
                    ExtraWindow extraWindow = new ExtraWindow(new LodingViewModel(), 200, 200);
                    Task.Run(()
                      =>
                  {
                      if (Randomizer.RandomKey.Equals(ConfirmationCode) && CurrentUser.Instance.User.Email != null
                          && CurrentUser.Instance.User.Id.Equals(new Guid("7b30161a-1d08-405e-b3f9-4f89661be70a")))
                      {
                          ConfirmationCode = String.Empty;
                          navigation.NavigateTo(ViewType.ForgetPass);
                      }
                      else if (Randomizer.RandomKey.Equals(ConfirmationCode))
                      {
                          ConfirmationCode = String.Empty;
                          navigation.NavigateTo(ViewType.SignUp);
                      }
                      else
                      {
                          ConfirmationCode = String.Empty;
                          Errors.ConfirmCodeErrorMsg();
                      }
                      CloseWindow();
                  });
                    WindowBluringCustom.Bluring();
                    extraWindow.ShowDialog();
                    WindowBluringCustom.Normal();
                }

            ));
    }
}
