using Asana.Model;
using Asana.Navigation;
using Asana.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using Asana.Objects;
using Asana.Services;

namespace Asana.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;
        private readonly ProjectService projectService;
        public AccountViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            Projects = new ObservableCollection<dynamic>();
            Header = new HeaderViewModel(navigationService);
            projectService = new ProjectService();
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

      
        private ObservableCollection<dynamic> projects;

        public ObservableCollection<dynamic> Projects
        {
            get { return projects; }
            set { Set(ref projects, value); }
        }

        private BitmapImage profileImage;

        public BitmapImage ProfileImage
        {
            get { return profileImage; }
            set { Set(ref profileImage, value); }
        }


        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { Set(ref phoneNumber, value); }
        }


        private RelayCommand _loadedCommand;

        public RelayCommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new RelayCommand((() => {
                Username = CurrentUser.Instance.User.Username;
                Email = CurrentUser.Instance.User.Email;
                PhoneNumber = "0772209966";

                ProfileImage = ProfilePhoto.ByteArrayToImage(CurrentUser.Instance.User.Image);
                projectService.GetCurrentUserProjects().ForEach(x => Projects.Add(new { Title = x.Name, x.Description }));

            })));
        }


    }







}
