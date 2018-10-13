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
    public class TaskService : ITaskService
    {
        public async System.Threading.Tasks.Task CreateAsync(Objects.Task task)
        {

            if (task != null)
            {
                try
                {
                    if (CurrentUser.Instance.User.FullName.Equals(CurrentProject.Instance.Project.ProjectManager))
                    {

                        using (var context = new AsanaDbContext())
                        {
                            int maxPosition = 0;
                            task.IsTaskAdded = true;
                            task.CreatedAt = DateTime.Now;
                            if (context.Tasks.Count() > 0)
                            {
                                maxPosition = context.Tasks.Max(x => x.Position) + 1;
                            }
                            task.Position = maxPosition;
                            task.CurrentKanbanState = context.KanbanState.FirstOrDefault(z => z.Name.Equals("In Progress")).Name;
                            context.Tasks.Add(task);
                            await context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        throw new Exception("You are not permitted to create task");

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public Objects.Task FindById(Guid taskId)
        {
            if (taskId != null)
            {
                try
                {
                    using (var context = new AsanaDbContext())
                    {
                        var task = context.Tasks.Include("Column")
                                                .Include("TaskLogs")
                                                .FirstOrDefault(x => x.Id == taskId);
                        if (task != null)
                        {
                            return task;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                return null;
            }
            throw new Exception("id is null");

        }

        public ICollection<KanbanState> GetKanbanStatesOfTask()
        {
            using (var asana = new AsanaDbContext())
            {
                return new ObservableCollection<KanbanState>(asana.KanbanState);
            }

        }

        public async System.Threading.Tasks.Task RemoveAsync(Objects.Task task)
        {
            if (task != null)
            {
                try
                {
                    if (CurrentUser.Instance.User.FullName.Equals(CurrentProject.Instance.Project.ProjectManager) ||
                        CurrentUser.Instance.User.FullName.Equals(task.AssignedTo))
                    {


                        using (var context = new AsanaDbContext())
                        {
                            var t = context.Tasks.FirstOrDefault(x => x.Id == task.Id);
                            if (t != null)
                            {
                                context.Tasks.Remove(t);

                            }
                            await context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        throw new Exception("You are not permitted to delete task.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }
        public async System.Threading.Tasks.Task UpdateAsync(Objects.Task task)
        {
            if (task != null)
            {
                try
                {
                    if (CurrentUser.Instance.User.FullName.Equals(CurrentProject.Instance.Project.ProjectManager)
                        || CurrentUser.Instance.User.FullName.Equals(task.AssignedTo))
                    {

                        using (var context = new AsanaDbContext())
                        {

                            context.Tasks.First(x => x.Id == task.Id).IsStarred = task.IsStarred;
                            context.Tasks.First(x => x.Id == task.Id).StarPath = task.StarPath;
                            context.Tasks.First(x => x.Id == task.Id).CurrentKanbanState = task.CurrentKanbanState;

                            context.Tasks.First(x => x.Id == task.Id).AssignedTo = task.AssignedTo;

                            context.Tasks.First(x => x.Id == task.Id).ColumnId = task.ColumnId;
                            context.Tasks.First(x => x.Id == task.Id).Deadline = task.Deadline;
                            context.Tasks.First(x => x.Id == task.Id).Image = task.Image;
                            context.Tasks.First(x => x.Id == task.Id).Description = task.Description;
                            context.Tasks.First(x => x.Id == task.Id).Title = task.Title;
                            await context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        throw new Exception("You are not permitted to edit the task.");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }


        public async System.Threading.Tasks.Task UpdateColumnId(Guid columnId, Objects.Task task)
        {
            if (task != null)
            {
                try
                {
                    if (CurrentUser.Instance.User.FullName.Equals(CurrentProject.Instance.Project.ProjectManager)
                         || CurrentUser.Instance.User.FullName.Equals(task.AssignedTo))
                    {
                        using (var context = new AsanaDbContext())
                        {

                            var t = context.Tasks.Include("Column").Include("TaskLogs").FirstOrDefault(x => x.Id == task.Id);

                            if (t != null)
                            {
                                var c = context.Columns.FirstOrDefault(x => x.Id == task.ColumnId);
                                if (c != null)
                                {
                                    context.Tasks.FirstOrDefault(x => x.Id == t.Id).ColumnId = columnId;
                                }
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("You are not permitted to edit the task.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}

