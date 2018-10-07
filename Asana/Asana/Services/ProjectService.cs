﻿using Asana.Model;
using Asana.Objects;
using Asana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.Services
{
    public class ProjectService : IProjectService
    {
        public void LoadProjects(Guid userId)
        {

            if (userId != null)
            {
                try
                {
                    using (var context = new AsanaDbContext())
                    {
                        var user = context.Users.FirstOrDefault(x => x.Id == userId);
                        var projects = (context.Projects.Include("Users")
                                                       .Include("Columns").Select(x => x.Users.Where(z => z.Email.Equals(user.Email)))) as ObservableCollection<Project>;
                                        
                                                 
                                                 
                        
                        if (projects != null)
                        {
                            ProjectsOfUser.Instance.Projects = projects;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }

        }

        public async System.Threading.Tasks.Task CreateAsync(Project project)
        {

            if (project != null)
            {

                try
                {
                    using (var context = new AsanaDbContext())
                    {
                        var p = context.Projects.FirstOrDefault(x => x.ProjectEmail.Equals(project.ProjectEmail));
                        if (p != null)
                        {
                            throw new Exception("Project with this email already exists. TRY another email!");
                        }
                        else
                        {
                            int maxPosition = 0;
                            if (context.Projects.Count()>0)
                            {
                                maxPosition = context.Projects.Max(x => x.Position);
                            }
                            project.Position = (maxPosition == 0) ? 0 : ++maxPosition;
                            context.Projects.Add(project);
                            context.UserRoles.Add(new UserRoles
                            {
                                Email = CurrentUser.Instance.User.Email,
                                ProjectId = project.Id,
                                FullName = project.ProjectManager
                            });
                            await context.SaveChangesAsync();
                        }
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
            if (project != null)
            {
                try
                {
                    using (var db = new AsanaDbContext())
                    {
                        var p = db.Projects.FirstOrDefault(x => x.Id == project.Id);
                        if (p != null)
                        {
                            db.Projects.Remove(p);

                        }
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
            if (userId != null)
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
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                        db.Projects.First(x => x.Id == project.Id).Description = project.Description;
                        db.Projects.First(x => x.Id == project.Id).Deadline = project.Deadline;

                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        public async System.Threading.Tasks.Task UpdatePositionAsync(int index, Project project)
        {
            if (project != null)
            {
                try
                {
                    using (var db = new AsanaDbContext())
                    {

                        var item = db.Projects.FirstOrDefault(x => x.Id == project.Id);
                        if (item != null)
                        {
                            db.Projects.First(x => x.Id == item.Id).Position = index;
                        }
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
