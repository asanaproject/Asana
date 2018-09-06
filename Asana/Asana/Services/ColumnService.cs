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
    public class ColumnService : IColumnService
    {
        public async System.Threading.Tasks.Task Add(Column column)
        {
            try
            {
                if (column==null)
                {
                    throw new Exception("column is null");
                }
                using (var context=new AsanaDbContext())
                {
                    //MessageBox.Show(column.Title);
                    //context.Users.First(x => x.Id == CurrentUser.Id)
                    //       .Projects.First(x => x.Id == CurrentProject.Instance.Project.Id)
                    //       .Project.Columns.Add(column);
                    context.Columns.Add(column);
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
