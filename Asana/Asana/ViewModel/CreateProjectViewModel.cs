using Asana.Navigation;
using Asana.Tools;
using Asana.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class CreateProjectViewModel :ViewModelBase
    {
        private readonly NavigationService navigation;

        public CreateProjectViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            header = new HeaderViewModel(navigation);
        }

        private object header;

        public object Header
        {
            get { return header; }
            set { Set(ref header, value); }
        }


        private RelayCommand createProject;

        public RelayCommand CreateProject => createProject ?? (createProject = new RelayCommand(
            () =>
            {
                WindowBluringCustom.Bluring();
                ExtraWindow extraWindow = new ExtraWindow(new ProjectAddViewModel(), 400, 400);
                extraWindow.ShowDialog();
                WindowBluringCustom.Normal();
            }
            ));

       
    }
}
