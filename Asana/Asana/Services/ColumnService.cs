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

            if (column != null)
            {
                try
                {
                    using (var context = new AsanaDbContext())
                    {
                       
                        context.Columns.Add(column);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public Column FindById(Guid columnId)
        {
            if (!String.IsNullOrWhiteSpace(columnId.ToString()))
            {
                try
                {
                    using (var context = new AsanaDbContext())
                    {
                        return context.Columns.First(x => x.Id == columnId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            return null;
        }

        public ObservableCollection<Column> GetAll(Guid projectId)
        {
            try
            {
                using (var context = new AsanaDbContext())
                {
                    return context.Columns
                                  .Include("Project")
                                  .Include("Tasks")
                                  .Where(x => x.Project.Id == projectId) as ObservableCollection<Column>;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                throw new Exception(ex.Message);
            }
        }

        public async System.Threading.Tasks.Task UpdateTitleAsync(string title, Column column)
        {
            if (!String.IsNullOrWhiteSpace(title) || column == null)
            {
                try
                {
                    using (var context = new AsanaDbContext())
                    {
                        context.Columns.First(x => x.Id == column.Id)
                                     .Title = title;
                        await context.SaveChangesAsync();
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

