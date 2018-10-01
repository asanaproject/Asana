using Asana.Model;
using Asana.Objects;
using Asana.Services.Interfaces;
using Asana.ViewModel;
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

        public ICollection<Column> GetAll(Guid projectId)
        {
            try
            {
                using (var context = new AsanaDbContext())
                {
                    return new ObservableCollection<Column>(context.Columns
                                  .Include("Project")
                                  .Include("Tasks")
                                  .Where(x => x.Project.Id == projectId));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                throw new Exception(ex.Message);
            }
        }

        public async System.Threading.Tasks.Task LoadColumns(Guid projectId)
        {
            await System.Threading.Tasks.Task.Run(
               () =>
               {
                   if (projectId != null)
                   {
                       try
                       {
                           using (var context = new AsanaDbContext())
                           {
                               var columns = GetAll(CurrentProject.Instance.Project.Id) as ObservableCollection<Column>;
                               if (columns != null)
                               {
                                   foreach (var item in columns)
                                   {
                                       ColumnsOfProject.Instance.Columns.Add(new ColumnItemViewModel { ColumnIsAdded = true, Column = item, Title = item.Title });
                                   }
                               }
                           }
                       }
                       catch (Exception ex)
                       {
                           MessageBox.Show(ex.Message);
                       }
                   }
               }
            );

        }

        public async System.Threading.Tasks.Task RemoveAsync(Column column)
        {
            if (column != null)
            {
                try
                {
                    using (var context = new AsanaDbContext())
                    {
                        context.Columns.Remove(column);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public async System.Threading.Tasks.Task UpdateAsync(int sourceIndex, int targetIndex, Column column)
        {
            if (column != null)
            {
                try
                {
                    using (var db = new AsanaDbContext())
                    {
                        db.Columns.ToList().RemoveAt(sourceIndex);
                        db.Columns.ToList().Insert(targetIndex, column);
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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

