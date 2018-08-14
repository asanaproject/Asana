using Asana.Navigation;
using Asana.Tools;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Asana.ViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        private readonly NavigationService navigation;
        public SignUpViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
        }

        private string profieImgPath;
        public string ProfileImgPath
        {
            get { return profieImgPath; }
            set
            {
                Set(ref profieImgPath, value);
            }
        }


        /// <summary>
        /// Command for come back to login view
        /// </summary>
        private RelayCommand _goToLogInCommand;
        public RelayCommand GoToLogInViewCommand => _goToLogInCommand ?? (_goToLogInCommand = new RelayCommand(
            x => navigation.NavigateTo(ViewType.LogIn)
            ));


        private RelayCommand _loadImageCommand;
        public RelayCommand LoadImageCommand => _loadImageCommand ?? (_loadImageCommand = new RelayCommand(
            x =>
            {
                var path = ProfilePhoto.LoadImage();
                if (!String.IsNullOrEmpty(path))
                {
                    ProfileImgPath = path;
                }
            }
            ));
    }
}
