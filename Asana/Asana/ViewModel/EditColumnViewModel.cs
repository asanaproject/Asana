using Asana.Model;
using Asana.Navigation;
using Asana.Services;
using Asana.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class EditColumnViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;
        private readonly IColumnService columnService;
        System.Timers.Timer timer;

        public EditColumnViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            columnService = new ColumnService();
            Title = CurrentColumn.Instance.Column.Title;
            ProjectTitle = CurrentProject.Instance.Project.Name;
            CreatedAt = "Created at: " + CurrentColumn.Instance.Column.Column.CreatedAt.Humanize();

            timer = new System.Timers.Timer(1000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;

        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CreatedAt = "Created at: " + CurrentColumn.Instance.Column.Column.CreatedAt.Humanize();

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
        private string title;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }
        private string createdAt;
        public string CreatedAt
        {
            get { return createdAt; }
            set { Set(ref createdAt, value); }
        }


        private string projectTitle;
        public string ProjectTitle
        {
            get { return projectTitle; }
            set { Set(ref projectTitle, value); }
        }

        /// <summary>
        /// Command closes window which is for editing column's title
        /// </summary>
        private RelayCommand _closeWindowCommand;
        public RelayCommand CloseWindowCommand => _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(
        () =>
        {
            Task.Run(() =>
            {
                timer.Stop();
                Closewindow();
            });

        }));


        /// <summary>
        /// submits changes about columns' title
        /// </summary>
        private RelayCommand submitCommand;
        public RelayCommand SubmitCommand => submitCommand ?? (submitCommand = new RelayCommand(
        () =>
        {
            columnService.UpdateTitle(Title, CurrentColumn.Instance.Column.Column);
            Task.Run(() =>
            {
                timer.Stop();
                Closewindow();
            });
        }));
    }
}
