using Asana.Tools;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asana.Navigation;
using Asana.Objects;

namespace Asana.ViewModel
{
    public class RegisterEmailViewModel:ViewModelBase
    {
        private readonly NavigationService navigation;
        public RegisterEmailViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }


        private RelayCommand _signUpBtnCommand;

        public RelayCommand SignUpCommand => _signUpBtnCommand ?? (_signUpBtnCommand = new RelayCommand(
            x =>
            {
                using (var db = new AsanaDbContext())
                {

                }
            }
            ));
    }
}
