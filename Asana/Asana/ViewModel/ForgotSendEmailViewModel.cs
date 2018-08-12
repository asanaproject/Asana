﻿using Asana.Model;
using Asana.Navigation;
using Asana.Tools;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.ViewModel
{
    public class ForgotSendEmailViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;

        public ForgotSendEmailViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; Set(ref email, value); }
        }


        private RelayCommand _sendCommand;

        public RelayCommand SendCommand => _sendCommand ?? (_sendCommand = new RelayCommand(
            x =>
            {

                if (RegexChecker.CheckEmail(Email))
                {
                    Task.Run(() =>
                    {
                        EmailHelper email = new EmailHelper();
                        email.SendForgotPasswordCode(Email);
                    });
                    CurrentUser current = CurrentUser.GetInstance();
                    current.Email = Email;
                    navigation.NavigateTo(ViewType.ForgetPass);
                }
            }
        ));

    }
}