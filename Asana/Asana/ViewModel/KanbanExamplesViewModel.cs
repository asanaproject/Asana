using Asana.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class KanbanExamplesViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;

        public KanbanExamplesViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public void Closewindow()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title == "ExtraWindow")
                        window.Close();
                }
            });
        }

        private RelayCommand _closeWindowCommand;

        public RelayCommand CloseWindowCommand => _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(
        () =>
        {
            Task.Run(() =>
            {
                Closewindow();
            });
        }));
    }
}
