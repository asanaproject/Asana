using Asana.Model;
using Asana.Navigation;
using Asana.Tools;
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
    public class TaskPageViewModel:ViewModelBase
    {
        private readonly NavigationService navigation;
        System.Timers.Timer timer;

        public TaskPageViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            TaskImgPath = CurrentTask.Instance.Task.Image==null ? "../Resources/Images/empty_task_img.png" : CurrentTask.Instance.Task.ImagePath;
            timer = new System.Timers.Timer(1000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;

        }
      

        private RelayCommand _loadImageCommand;
        public RelayCommand LoadImageCommand => _loadImageCommand ?? (_loadImageCommand = new RelayCommand(
            () =>
            {
                var path = ProfilePhoto.LoadImage();
                if (!String.IsNullOrEmpty(path))
                {
                    TaskImgPath= path;
                }
            }
            ));
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
        private string createdAt;
        public string CreatedAt
        {
            get { return createdAt; }
            set { Set(ref createdAt, value); }
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

        private string taskImgPath;
        public string TaskImgPath
        {
            get { return taskImgPath; }
            set {Set(ref taskImgPath,value); }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CreatedAt = "Created at: " + CurrentTask.Instance.Task.CreatedAt.Humanize();

        }
    }

}
