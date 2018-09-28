using Asana.Model;
using Asana.Navigation;
using Asana.Services;
using Asana.Services.Interfaces;
using Asana.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class AssignToNewUserViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly NavigationService navigation;
        private readonly ITaskService taskService;
        private readonly IUserRoleService userRoleService;
        public AssignToNewUserViewModel(NavigationService navigation)
        {
            this.navigation = navigation;
            taskService = new TaskService();
            userRoleService = new UserRoleService();
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { Set(ref fullName, value); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { Set(ref email, value); }
        }

        private string mobile;
        public string Mobile
        {
            get { return mobile; }
            set { Set(ref mobile, value); }
        }


        /// <summary>
        /// command for closing window of AssignToNewUserView
        /// </summary>
        private RelayCommand _closeWindowCommand;
        public RelayCommand CloseWindowCommand => _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(
        () =>
        {
            Task.Run(() =>
            {
                Closewindow();
            });

        }));

        /// <summary>
        /// command for closing window of AssignToNewUserView
        /// </summary>
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand => saveCommand ?? (saveCommand = new RelayCommand(
        () =>
        {
            Task.Run(() =>
            {
                var user = new UserRoles
                {
                    FullName=FullName,
                    Email = Email,
                    Phone = Mobile,
                    ProjectId = CurrentProject.Instance.Project.Id
                };
                userRoleService.CreateAsync(user);
                Closewindow();
            });

        }));

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {

            get
            {
                string result = String.Empty;
                if (columnName.Equals(nameof(FullName)))
                {
                    if (String.IsNullOrWhiteSpace(FullName))
                    {
                        result = "Enter fullname of employee.";
                    }
                }
                else if (columnName.Equals(nameof(Email)))
                {
                    if (!RegexChecker.CheckEmail(Email))
                    {
                        result = "Email format is not correct.";
                    }
                }
                else if (columnName.Equals(nameof(Mobile)))
                {
                    if (!RegexChecker.CheckMobileTelephone(Mobile))
                    {
                        result = "Contact number format is not correct.";
                    }
                }
                return result;
            }
        }

        public void Closewindow()
        {

            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title.Equals("ExtraWindow"))
                        window.Close();
                }
            });
        }
    }


}
