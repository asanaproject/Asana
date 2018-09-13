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


        public ProjectAddViewModel()
        {
            deadline = DateTime.Now;
        }

        private string projectName;

        public string ProjectName
        {
            get { return projectName; }
            set { Set(ref projectName, value); }
        }


        private string assignTo;

        public string AssignTo
        {
            get { return assignTo; }
            set { Set(ref assignTo, value); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { Set(ref description, value); }
        }

        private DateTime deadline;

        public DateTime Deadline
        {
            get { return deadline; }
            set { Set(ref deadline, value); }
        }



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
