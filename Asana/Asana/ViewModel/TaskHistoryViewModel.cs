using Asana.Navigation;
using Asana.Services;
using Asana.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class TaskHistoryViewModel:ViewModelBase
    {
        private readonly NavigationService navigation;
        public TaskHistoryViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }

        public void Closewindow()
        {

            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title.Equals("ExtraWindow"))
                        window.Close();
                }
            });
        }

        private RelayCommand _closeWindowCommand;
        public RelayCommand CloseWindowCommand => _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(
        () =>
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Closewindow();
            });

        }));
    }
}
