using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asana.Navigation;
using Asana.Tools;
using System.Windows;

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

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; Set(ref _email, value); }
        }

        private bool _newPassIsEnable;

        public bool NewPassIsEnable
        {
            get { return _newPassIsEnable; }
            set { _newPassIsEnable = value; Set(ref _newPassIsEnable, value); }
        }

        private string _confirmationCode;
        public string ConfirmationCode
        {
            get { return _confirmationCode; }
            set
            {
                Set(ref _confirmationCode, value);
            }
        }


        private Visibility applyButton;
        public Visibility ApplyButton
        {
            get { return applyButton; }
            set { Set(ref applyButton, value); }
        }

        private Visibility resetButton;
        public Visibility ResetButton
        {
            get { return resetButton; }
            set { Set(ref resetButton, value); }
        }



        private RelayCommand _newPassCommand;

        public RelayCommand NewPassCommand => _newPassCommand ?? (_newPassCommand = new RelayCommand(
            x =>
            {
                if (Randomizer.RandomKey.Equals(ConfirmationCode))
                {
                }

            }));


        private RelayCommand _emailCheckCommand;

        public RelayCommand EmailCheckCommand => _emailCheckCommand ?? (_emailCheckCommand = new RelayCommand(
            x =>
            {
                emailHelper.SendForgotPasswordCode(Email);
               
                    NewPassIsEnable = true;
                ApplyButton = Visibility.Visible;
                resetButton = Visibility.Hidden;
                
                
            }));


        private RelayCommand _cancelCommand;

        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(
            x => navigation.NavigateTo(ViewType.LogIn)
            ));


    }
}
