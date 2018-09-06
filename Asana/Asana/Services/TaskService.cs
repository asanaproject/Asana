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
    }
}
