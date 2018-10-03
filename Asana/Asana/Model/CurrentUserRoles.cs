using Asana.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class CurrentUserRoles : ViewModelBase
    {
        private CurrentUserRoles()
        {

        }
        private static CurrentUserRoles instance;
        public static CurrentUserRoles Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentUserRoles();
                }
                return instance;
            }
        }
        private ObservableCollection<UserRoles> employees;
        public ObservableCollection<UserRoles> Employees
        {
            get { return employees; }
            set { Set(ref employees, value); }
        }

    }
}
