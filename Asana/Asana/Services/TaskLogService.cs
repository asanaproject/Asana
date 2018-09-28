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
    public class TaskLogService : ITaskLogService
    {
        public async System.Threading.Tasks.Task CreateAsync(TaskLog log)
        {
            if (log != null)
            {
                try
                {
                    using (var context = new AsanaDbContext())
                    {
                        context.Columns.First(x => x.Id == log.Task.ColumnId).Tasks.First(x => x.Id == log.Task.Id).TaskLogs.Add(log);
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
}
