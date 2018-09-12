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
    public class ProjectAddViewModel : ViewModelBase
    {


        public ProjectAddViewModel() { }


        private RelayCommand closewindow;

        public RelayCommand Closewindow => closewindow ?? (closewindow = new RelayCommand(
            () =>
            {
                Task.Run(() =>
                {
                    CloseWindow();
                });

            }));

        public void CloseWindow()
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

    }
}
