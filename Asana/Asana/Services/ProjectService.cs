using Asana.Model;
using Asana.Objects;
using Asana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public async System.Threading.Tasks.Task RemoveAsync(Project project)
        {
            if (project!=null)
            {
                try
                {
                    using (var db = new AsanaDbContext())
                    {
                        db.Projects.Remove(project);
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }               

            }
        }

        public ICollection<Project> GetAll(Guid userId)
        {
            if (userId!=null)
            {
                try
                {
                    using (var db = new AsanaDbContext())
                    {
                        return new ObservableCollection<Project>(db.Projects.Include("User")
                                          .Include("Columns")
                                          .Include("Users").Where(x => x.UserId == userId));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
                }

            }
            return null;
           
        }

        public async System.Threading.Tasks.Task UpdateAsync(Project project)
        {
            if (project != null)
            {
                try
                {
                    using (var db = new AsanaDbContext())
                    {
                        db.Projects.First(x => x.Id == project.Id).Name = project.Name;
                        db.Projects.First(x => x.Id == project.Id).ProjectEmail = project.ProjectEmail;
                        db.Projects.First(x => x.Id == project.Id).ProjectManager = project.ProjectManager;
                        db.Projects.First(x => x.Id == project.Id).UserId = project.UserId;
                        db.Projects.First(x => x.Id == project.Id).Columns = project.Columns;
                        db.Projects.First(x => x.Id == project.Id).Description = project.Description;
                        db.Projects.First(x => x.Id == project.Id).Users = project.Users;                                                                                          
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        //public List<Prsoject> GetCurrentUserProjects()
        //{
        //    List<Project> Projects = new List<Project>();
        //    using (var db = new AsanaDbContext())
        //        Projects = db.Projects.Where(x => x.UserId == CurrentUser.Id).ToList();
        //    return Projects;
        //}
    }
}
