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
        public LoginView()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            ForgotPasTxt.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void ForgotPasTxt_MouseLeave(object sender, MouseEventArgs e)
        {
            ForgotPasTxt.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void LoginTxtBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginTxtBox.Text = String.Empty;
        }

        private void LoginTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTxtBox.Text))
                LoginTxtBox.Text = "Email";
        }

        private void CreateTxtBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            CreateTxtBlock.Foreground = new SolidColorBrush(Colors.Green);
        }

        private void CreateTxtBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            CreateTxtBlock.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
