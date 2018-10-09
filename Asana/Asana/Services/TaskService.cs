﻿using Asana.Model;
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
                    if (!CurrentUser.Instance.User.FullName.Equals(CurrentProject.Instance.Project.ProjectManager))
                    {
                        throw new Exception("You are not permitted to create task");
                    }
                    if (String.IsNullOrEmpty(task.AssignedTo))
                    {
                        throw new Exception("You must assign the task to someone.");
                    }
                    else
                    {
                        using (var context = new AsanaDbContext())
                        {
                            int maxPosition = 0;
                            if (context.Tasks.Count() > 0)
                            {
                                maxPosition = context.Tasks.Max(x => x.Position) + 1;
                            }
                            task.Position = maxPosition;
                            context.Tasks.Add(task);
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

        public Objects.Task FindById(Guid taskId)
        {
            if (taskId != null)
            {
                try
                {
                    using (var context = new AsanaDbContext())
                    {
                        var task = context.Tasks.Include("Column")
                                                .Include("TaskKanbanStates")
                                                .Include("CurrentKanbanState")
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
                throw new Exception("Task with this id doesn't exist.");
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
                            context.Tasks.Remove(task);
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
                            context.Tasks.First(x => x.Id == task.Id).ColumnId = task.ColumnId;
                            context.Tasks.First(x => x.Id == task.Id).Deadline = task.Deadline;
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

        public async System.Threading.Tasks.Task UpdateAsyncKanbanState(Objects.Task task, TaskKanbanState s)
        {
            try
            {
                if (CurrentUser.Instance.User.FullName.Equals(CurrentProject.Instance.Project.ProjectManager)
                       || CurrentUser.Instance.User.FullName.Equals(task.AssignedTo))
                {
                    using (var asana = new AsanaDbContext())
                    {
                        var y = asana.Tasks.First(x => x.Id == task.Id);
                        MessageBox.Show(y.Id.ToString());
                        asana.TaskKanbanState.Add(s);
                        await asana.SaveChangesAsync();
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

                            var t = context.Tasks.FirstOrDefault(x => x.Id == task.Id);
                            if (t != null)
                            {
                                var c = context.Columns.FirstOrDefault(x => x.Id == task.ColumnId);
                                if (c != null)
                                {
                                    context.Tasks.Remove(t);
                                    await context.SaveChangesAsync();
                                    t.ColumnId = columnId;
                                    context.Tasks.Add(t);
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

