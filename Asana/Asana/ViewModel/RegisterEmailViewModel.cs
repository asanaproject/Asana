using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Asana.ViewModel
{
    public class RegisterEmailViewModel:ViewModelBase
    {
        private readonly NavigationService navigation;
        public RegisterEmailViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }
    }
}
