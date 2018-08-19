using Asana.Navigation;
using Asana.Tools;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
   public class ConfirmCodeViewModel:ViewModelBase
    {
        private readonly NavigationService navigation;

        public ConfirmCodeViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }

        private RelayCommand _backCommand;
        public RelayCommand BackCommand => _backCommand ?? (_backCommand = new RelayCommand(
            x => navigation.NavigateTo(ViewType.LogIn)
            ));
         

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
        /// command checks sameness of inputted code and code which is sent to user's email
        /// </summary>
        private RelayCommand confirmCommand;
        public RelayCommand ConfirmCommand
        {
            get => confirmCommand ?? (confirmCommand = new RelayCommand(
                x =>
                {
                    if (Randomizer.RandomKey.Equals(ConfirmationCode))
                    {
                        navigation.NavigateTo(ViewType.SignUp);
                    }
                    else
                    {
                        Errors.ConfirmCodeErrorMsg();
                    }
                }
            ));
        }
    }
}
