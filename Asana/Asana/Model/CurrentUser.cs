using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class CurrentUser
    {
        public static string Username { get; set; } = "{{Username}}";
        public static Guid Id { get; set; }
        private readonly IProjectService projectService;
        private User user = new User();

        public User User
        {
            get { return user; }
            set { user = value; if (value.Username != null) { Username = user.Username; Id = user.Id; } }
        }

        private CurrentUser()
        {
            projectService = new ProjectService();
           // ProjectsOfUser.Instance.Projects = projectService.GetAll(CurrentUser.Instance.User.Id) as ObservableCollection<Project>;
           // ColumnsOfProject.Instance.Columns;

        }
        private static CurrentUser instance;
        public static CurrentUser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentUser();
                }
                return instance;

            }
        }

        //public void SetProps(User info)
        //{
        //    currenUser = info;
        //}
    }
}
