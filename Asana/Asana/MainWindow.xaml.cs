using Asana.Tools;
using Asana.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Asana
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Settings.SetDefaultSettings();
        }

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            main.Content = loginView;
        }
    }
}
