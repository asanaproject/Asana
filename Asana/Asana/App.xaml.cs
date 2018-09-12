using Asana.View;
using Asana.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace Asana
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Timer connectionTimer;

        public App()
        {
            connectionTimer = new Timer(10000);
            connectionTimer.Elapsed += ConnectionTimer_Elapsed;
            var viewModelLocator = new ViewModelLocator();
            var app = new AppView();
            app.DataContext = viewModelLocator.appViewModel;
            connectionTimer.Start();
            app.ShowDialog();
        }

        private void ConnectionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WebClient client = new WebClient();
            try
            {
                using (client.OpenRead("http://www.google.com"))
                {
                }
            }
            catch (WebException)
            {
                MessageBox.Show("Check Your Connection!!");
                App.Current.Dispatcher.Invoke(() => Application.Current.MainWindow.Close());
            }
        }
    }
}
