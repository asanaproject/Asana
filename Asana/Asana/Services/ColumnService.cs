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
    public class ColumnService : IColumnService
    {
        public async System.Threading.Tasks.Task CreateAsync(Column column)
        {
            try
            {
                if (column == null)
                {
                    throw new Exception("column is null");
                }
                using (var context = new AsanaDbContext())
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

        public ObservableCollection<Column> GetAll(Guid projectId)
        {
            using (var context = new AsanaDbContext())
            {
                return  context.Projects.Select(x => x.Columns.Select(y => y.ProjectId == projectId)) as ObservableCollection<Column>;
            }
        }

        public async System.Threading.Tasks.Task UpdateTitleAsync(string title, Column column)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(title) || column == null)
                {
                    throw new Exception("Column/title is null");
                }
                using (var context = new AsanaDbContext())
                {
                    context.Columns.First(x => x.Id == column.Id).Title = title;
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
