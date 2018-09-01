using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Asana.Tools
{
    //Created By Ali :))))) (Not Copied)
    public class WindowBluringCustom
    {
        private static Window blurWindow;

        public static void Bluring()
        {
            blurWindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = Application.Current.MainWindow.WindowState,
                Width = Application.Current.MainWindow.Width,
                Height = Application.Current.MainWindow.Height,
                Left = Application.Current.MainWindow.Left,
                Top = Application.Current.MainWindow.Top
            };
            blurWindow.Show();

        }
        public static void Normal()
        {
            blurWindow.Close();
        }
    }
}
