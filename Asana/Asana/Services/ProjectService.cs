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
            try
            {
                if(project == null)
                {
                
                    throw new Exception("Project is Null");
                }

                using(var context = new AsanaDbContext())
                {
                    //context.Users.First(x => x.Id == CurrentUser.Id)
                    //    .Projects.First(p => p.Project.Id == CurrentProject.Instance.Project.Id)
                    //    .add

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex) 
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
