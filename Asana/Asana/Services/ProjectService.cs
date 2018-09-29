using Asana.Model;
using Asana.Objects;
using Asana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.Services
{
    public class ProjectService : IProjectService
    {

        public async System.Threading.Tasks.Task CreateAsync(Project project)
        {

            if (project != null)
            {

                try
                {
                    using (var context = new AsanaDbContext())
                    {
                        context.Projects.Add(project);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        public List<Project> GetCurrentUserProjects()
        {
            List<Project> Projects = new List<Project>();
            using (var db = new AsanaDbContext())
                Projects = db.Projects.Where(x => x.UserId == CurrentUser.Id).ToList();
            return Projects;
        }
    }
}
