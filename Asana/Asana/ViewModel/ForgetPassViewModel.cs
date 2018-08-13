using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asana.Navigation;
using Asana.Tools;

namespace Asana.ViewModel
{
    public class ForgetPassViewModel : ViewModelBase
    {
        EmailHelper emailHelper = new EmailHelper();
        private readonly NavigationService navigation;
        public ForgetPassViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            ApplyButton = Visibility.Hidden;
            NewPassIsEnable = false;
            ResetButton = Visibility.Visible;
        }

        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; Set(ref _code, value); }
        }

        private string _newPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; Set(ref _newPassword, value); }
        }

        private RelayCommand _emailCheckCommand;

        public RelayCommand EmailCheckCommand => _emailCheckCommand ?? (_emailCheckCommand = new RelayCommand(
            x =>
            {
                emailHelper.SendForgotPasswordCode(Email);

            } ));


        private RelayCommand _cancelCommand;

        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            x =>navigation.NavigateTo(ViewType.LogIn)
            ));


        private RelayCommand _resetCommand;

        public RelayCommand ResetComamnd => _resetCommand ?? (_resetCommand = new RelayCommand(
            x =>
            {
                if (Randomizer.RandomKey.Equals(Code))
                    try
                    {
                        using (var db = new AsanaDbContext())
                        {
                            string email = CurrentUser.GetInstance().Email;
                            var user = db.ExtraInfos.Single(users => users.Email == email);
                            user.Password = Hasher.EncryptString(_newPassword);
                        }
                    }
                    catch (Exception error)
                    {
                        Log.Error(error.Message);
                    }
                else
                    MessageBox.Show("Confirmation Code is not correct, enter it correctly!",
                                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                navigation.NavigateTo(ViewType.LogIn);
            }
            ));
    }

}
