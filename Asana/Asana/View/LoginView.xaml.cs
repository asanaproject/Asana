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

namespace Asana.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        private Visibility showPassword = Visibility.Hidden;
        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(showPassword == Visibility.Hidden)
            {
                showPassword = Visibility.Visible;
                PassBox.Visibility = Visibility.Hidden;
                PassTb.Visibility = showPassword;
            }
            else
            {
                showPassword = Visibility.Hidden;
                PassBox.Visibility = Visibility.Visible;
                PassTb.Visibility = showPassword;
            }
        }
    }
}
