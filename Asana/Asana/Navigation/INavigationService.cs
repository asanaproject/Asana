using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Navigation
{
    public interface INavigationService
    {
        ViewModelBase Current { get; set; }
        void NavigateTo(ViewType name);
        void GoBack();
        void ClearHistory();
        void AddPage(ViewModelBase page, ViewType name);
        void RemovePage(ViewType name);
    }
}
