using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asana.Navigation;
using Asana.Tools;
using System.Windows;
using Asana.Objects;
using Asana.Model;
using Serilog;

namespace Asana.ViewModel
{
    public class ForgetPassViewModel : ViewModelBase
    {
        EmailHelper emailHelper = new EmailHelper();
        private readonly NavigationService navigation;
        public ForgetPassViewModel(NavigationService navigation)
        {
            this.navigation = navigation;

        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; Set(ref _email, value); }
        }



        private string _reEnterpassword;

        public string ReEnterPassword
        {
            get { return _reEnterpassword; }
            set
            {
                _reEnterpassword = value;
                Set(ref _reEnterpassword, value);
            }
        }

        private string _newpassword;

        public string NewPassword
        {
            get { return _newpassword; }
            set
            {
                Set(ref _newpassword, value);
            }
        }


        private RelayCommand _newPassAplyCommand;

        public RelayCommand NewPassAplyCommand => _newPassAplyCommand ?? (_newPassAplyCommand = new RelayCommand(
            x =>
            {
                if (NewPassword.Equals(ReEnterPassword) && RegexChecker.CheckPassword(NewPassword))
                {


                    try
                    {
                        using (var db = new AsanaDbContext())
                        {
                            string email = CurrentUser.Instance.User.Email;
                            var user = db.ExtraInfos.Single(users => users.Email == email);
                            user.Password = Hasher.EncryptString(NewPassword);
                            navigation.NavigateTo(ViewType.LogIn);
                        }
                    }
                    catch (Exception error)
                    {
                        Log.Error(error.Message);
                    }
                }
                else
                    MessageBox.Show("Passwords are not same , enter it correctly!",
                                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
               

            }));



        private RelayCommand _cancelCommand;

        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            x => navigation.NavigateTo(ViewType.ForgotEmailCode)
            ));


    }
}
