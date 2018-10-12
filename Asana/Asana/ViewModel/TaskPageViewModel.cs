using Asana.Model;
using Asana.Navigation;
using Asana.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Asana.ViewModel
{
    public class TaskPageViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;
        System.Timers.Timer timer;

        public TaskPageViewModel(NavigationService navigation)
        {
            this.navigation = navigation;

            if (CurrentTask.Instance.Task.Image == null)
            {
                TaskImgPath = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory + "\\empty_task_img.png")));
            }
            else
            {
                TaskImgPath = ProfilePhoto.ByteArrayToImage(CurrentTask.Instance.Task.Image);
            }

            ColumnTitle = CurrentColumn.Instance.Column.Title;
            TaskTitle = CurrentTask.Instance.Task.Title;
            Deadline = CurrentTask.Instance.Task.Deadline;
            AssignedTo = CurrentTask.Instance.Task.AssignedTo;
            Description = CurrentTask.Instance.Task.Description;
            CreatedAt = "Created at: " + CurrentTask.Instance.Task.CreatedAt.Humanize();
            CurrentKanbanState =  CurrentProject.Instance.KanbanStates.FirstOrDefault(x=>x.Name.Equals(CurrentTask.Instance.Task.CurrentKanbanState)).Name;


            timer = new System.Timers.Timer(1000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;

        }
        private string columnTitle;
        public string ColumnTitle
        {
            get { return columnTitle; }
            set { Set(ref columnTitle, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { Set(ref description, value); }
        }

        private string assignedTo;
        public string AssignedTo
        {
            get { return assignedTo; }
            set { Set(ref assignedTo, value); }
        }


        private string currentKanbanState;
        public string CurrentKanbanState
        {
            get { return currentKanbanState; }
            set { Set(ref currentKanbanState, value); }
        }

        private DateTime? deadline;
        public DateTime? Deadline
        {
            get { return deadline; }
            set { Set(ref deadline, value); }
        }

        private string taskTitle;
        public string TaskTitle
        {
            get { return taskTitle; }
            set { Set(ref taskTitle, value); }
        }


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

        private BitmapImage taskImgPath;
        public BitmapImage TaskImgPath
        {
            get { return taskImgPath; }
            set { Set(ref taskImgPath, value); }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CreatedAt = "Created at: " + CurrentTask.Instance.Task.CreatedAt.Humanize();

        }
    }

}
