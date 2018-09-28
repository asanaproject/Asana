using Asana.Navigation;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asana.Model;
using GalaSoft.MvvmLight.Command;
using Asana.Objects;
using System.Collections.ObjectModel;

namespace Asana.ViewModel
{
    public class ProfileViewModel :ViewModelBase
    {
        private readonly NavigationService navigationService;

        public ProfileViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            Projects = new ObservableCollection<dynamic>();
            Header = new HeaderViewModel(navigationService);
        }
        private object header;

        public object Header
        {
            get { return header; }
            set { header = value; Set(ref header, value); }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { Set(ref username, value); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { Set(ref email, value); }
        }

        private string phonenumber;

        public string PhoneNumber
        {
            get { return phonenumber; }
            set { Set(ref phonenumber,value); }
        }

        private ObservableCollection<dynamic> projects;

        public ObservableCollection<dynamic> Projects
        {
            get { return projects; }
            set { Set(ref projects, value); }
        }


        private RelayCommand _loadedCommand;

        public RelayCommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new RelayCommand((() => {
                Username = CurrentUser.Instance.User.Username;
                Email = CurrentUser.Instance.User.Email;
                PhoneNumber = "0772209966";
                Projects.Add(new { Title = "ItSolution", Description = "Very Very Big Company! :)" , Created_Date="23.08.2001 16:58"});
                Projects.Add(new { Title = "ItSolution", Description = "Very Very Big Company! :)" , Created_Date="23.08.2001 16:59"});
                Projects.Add(new { Title = "ItSolution", Description = "Very Very Big Company! :)" , Created_Date="23.08.2001 17:00"});

                //using (var db = new AsanaDbContext())
                //    db.Projects.Where(x => x.Users.Any(y => y.Id == CurrentUser.Instance.User.Id)).ToList().ForEach(x => Projects.Add(x));
            })));
        }


    }
}
