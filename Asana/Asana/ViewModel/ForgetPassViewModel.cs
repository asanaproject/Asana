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
        private readonly NavigationService navigation;
        public ForgetPassViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }

        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; Set(ref code, value); }
        }

        private string newpassword;

        public string NewPassword
        {
            get { return newpassword; }
            set { newpassword = value; Set(ref newpassword, value); }
        }

        private string newpasswordconfirm;

        public string NewPasswordConfirm
        {
            get { return newpasswordconfirm; }
            set { newpasswordconfirm = value; Set(ref newpasswordconfirm, value); }
        }

        private RelayCommand _cancelCommand;

        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            x => navigation.NavigateTo(ViewType.LogIn)
            ));


        private RelayCommand _resetCommand;

        public RelayCommand ResetComamnd => _resetCommand ?? (_resetCommand = new RelayCommand(
            x =>
            {
                if (Code == Randomizer.RandomKey)
                    try
                    {

                        using (var db = new AsanaDbContext())
                        {
                            string email = CurrentUser.GetInstance().Email;
                            var user = db.ExtraInfos.Single(users => users.Email == email);
                            user.Password = Hasher.EncryptString(newpassword);
                        }
                    }
                    catch (Exception error)
                    {
                        Log.Error(error.Message);
                    }
                navigation.NavigateTo(ViewType.LogIn);
            }
            ));
        }
}
