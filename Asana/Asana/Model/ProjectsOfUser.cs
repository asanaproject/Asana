using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class ProjectsOfUser:ViewModelBase
    {
        private readonly IProjectService projectService;

        private ProjectsOfUser()
        {
            projectService = new ProjectService();
            Projects = projectService.GetAll(CurrentUser.Instance.User.Id) as ObservableCollection<Project>;

        }
        private static ProjectsOfUser instance;
        public static ProjectsOfUser Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new ProjectsOfUser();
                }
                return instance;
            }
        }
        private ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects
        {
            get { return projects; }
            set { Set(ref projects,value); }
        }

    }
}
