using Asana.Tools;
using Asana.ViewModel;
using GalaSoft.MvvmLight;
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

namespace Asana.View
{
    /// <summary>
    /// Interaction logic for ExtraWindow.xaml
    /// </summary>
    public partial class ExtraWindow : Window
    {
        public ExtraWindow(ViewModelBase view,double width=800,double height=450)
        {
            InitializeComponent();
            Width = width;
            Height = height;
            this.DataContext = new ExtraWindowViewModel(view);
        }
    }
}
