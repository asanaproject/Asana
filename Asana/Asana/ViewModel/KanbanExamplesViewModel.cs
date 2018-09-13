using Asana.Navigation;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.ViewModel
{
    public class KanbanExamplesViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;

        public KanbanExamplesViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
