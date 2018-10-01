using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Services.Interfaces
{
    public interface IColumnService
    {
        System.Threading.Tasks.Task CreateAsync(Column column);
        System.Threading.Tasks.Task RemoveAsync(Column column);
        System.Threading.Tasks.Task UpdateTitleAsync(string title, Column column);
        System.Threading.Tasks.Task UpdateAsync(int sourceIndex, int targetIndex,Column column);
        ICollection<Column> GetAll(Guid projectId);
        Column FindById(Guid columnId);
        System.Threading.Tasks.Task LoadColumns(Guid projectId);
    }
}

