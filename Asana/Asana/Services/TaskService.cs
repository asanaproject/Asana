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
    public class TaskService : ITaskService
    {
        AsanaDbContext context = new AsanaDbContext();
        public async System.Threading.Tasks.Task Add(Objects.Task task)
        {
            try
            {
                if (task == null)
                {
                    throw new Exception("Task is null");
                }
                using (context=new AsanaDbContext())
                {
                    //context.Users.First(x => x.Id == CurrentUser.Id)
                    //       .Projects.First(x => x.Id == CurrentProject.Instance.Project.Id)
                    //       .Project.Columns.First(x => x.Id == task.ColumnId)
                    //       .Tasks.Add(task);
                   await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
           
        }

        public async System.Threading.Tasks.Task Update(Objects.Task task)
        {
            if (task!=null)
            {
                try
                {
                    context.Tasks.First(x => x.Id == task.Id).Column = task.Column;
                    context.Tasks.First(x => x.Id == task.Id).ColumnId = task.ColumnId;
                    context.Tasks.First(x => x.Id == task.Id).CreatedAt = task.CreatedAt;
                    context.Tasks.First(x => x.Id == task.Id).Deadline = task.Deadline;
                    context.Tasks.First(x => x.Id == task.Id).Description = task.Description;
                    context.Tasks.First(x => x.Id == task.Id).ExtraInfo = task.ExtraInfo;
                    context.Tasks.First(x => x.Id == task.Id).ExtraInfoId = task.ExtraInfoId;
                    context.Tasks.First(x => x.Id == task.Id).IsTaskAdded = task.IsTaskAdded;
                    context.Tasks.First(x => x.Id == task.Id).KanbanState = task.KanbanState;
                    context.Tasks.First(x => x.Id == task.Id).KanbanStateId = task.KanbanStateId;
                    context.Tasks.First(x => x.Id == task.Id).Title = task.Title;
                    context.Tasks.First(x => x.Id == task.Id).IsStarred = task.IsStarred;
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
