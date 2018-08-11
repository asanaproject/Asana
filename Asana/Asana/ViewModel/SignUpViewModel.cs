using Asana.Navigation;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Asana.ViewModel
{
    public class SignUpViewModel:ViewModelBase
    {
        private readonly NavigationService navigation;
        public SignUpViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }
    }
}
