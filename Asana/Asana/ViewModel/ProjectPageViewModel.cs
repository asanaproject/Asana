
using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class ProjectPageViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;
        public ProjectPageViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            Column = new Column();
        }
        private Column column;
        public Column Column
        {
            get { return column; }
            set { Set(ref column, value); }
        }

     
        
        private RelayCommand createTaskCommand;
        public RelayCommand CreateTaskCommand => createTaskCommand ?? (createTaskCommand = new RelayCommand(
            () =>
            {
                using (AsanaDbContext context=new AsanaDbContext())
                {
                    context.Users.Find(CurrentProject.Instance.Project.Columns.Add(Column))
                }
                Columns.Add(new Column());
            }
));
    }
}
