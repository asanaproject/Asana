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
                    if (String.IsNullOrEmpty(task.AssignedTo))
                    {
                        throw new Exception("You must assign the task to someone.");
                    }
                    else
                    {
                        using (var context = new AsanaDbContext())
                        {
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
                    using (var context = new AsanaDbContext())
                    {
                        context.Tasks.Remove(task);
                        await context.SaveChangesAsync();
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
                using (var asana = new AsanaDbContext())
                {

                    var y = asana.Tasks.First(x => x.Id == task.Id);

                    MessageBox.Show(y.Id.ToString());
                    asana.TaskKanbanState.Add(s);
                    await asana.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

