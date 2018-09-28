using Asana.Navigation;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.ViewModel
{
    public class TaskHistoryViewModel:ViewModelBase
    {
        private readonly NavigationService navigation;
        public TaskHistoryViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }
    }
}
