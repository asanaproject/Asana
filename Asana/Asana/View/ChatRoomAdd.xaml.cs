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
using System.Windows.Shapes;

namespace Asana.ViewModel
{
    /// <summary>
    /// Interaction logic for ChatRoomAdd.xaml
    /// </summary>
    public partial class ChatRoomAdd : Window
    {
        public string GetName()
        {
            return Name.Text;
        }

        public ChatRoomAdd(string Title)
        {
            InitializeComponent();
            this.Title.Text = Title;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
