using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Asana.ViewModel
{
    public class AppViewModel : ViewModelBase
    {
        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(ref currentViewModel, value); }
        }

        public AppViewModel() => Messenger.Default.Register<ViewModelBase>(this,
                param => CurrentViewModel = param);


        #region Commands
        private RelayCommand _minimizeCommand;
        public RelayCommand MinimizeCommand
        {
            get => _minimizeCommand ?? (_minimizeCommand = new RelayCommand(
                (() => App.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized)));
        }


        private RelayCommand _maximizeCommand;
        public RelayCommand MaximizeCommand
        {
            get => _maximizeCommand ?? (_maximizeCommand = new RelayCommand(
                (() =>
                {
                    if (App.Current.MainWindow.WindowState == System.Windows.WindowState.Maximized) { 
                        App.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
                        App.Current.MainWindow.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        App.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
                        App.Current.MainWindow.WindowState = WindowState.Maximized;
                    }
                })));
        }


        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get => _closeCommand ?? (_closeCommand = new RelayCommand(
                (() => App.Current.MainWindow.Close())));
        }
        private RelayCommand _menuCommand;
        public RelayCommand MenuCommand
        {
            get => _menuCommand ?? (_menuCommand = new RelayCommand(
                (() => SystemCommands.ShowSystemMenu(App.Current.MainWindow, GetMousePosition()))));
        }

        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(App.Current.MainWindow);

            // Add the window position so its a "ToScreen"
            return new Point(position.X + App.Current.MainWindow.Left, position.Y + App.Current.MainWindow.Top);
        }
        #endregion
    }
}
