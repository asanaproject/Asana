using Asana.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Services.Interfaces
{
    public interface ITaskService
    {
        Task CreateAsync(Objects.Task task);
        Task RemoveAsync(Objects.Task task);
        Task UpdateAsync(Objects.Task task);
        Task UpdateColumnId(Guid columnId, Objects.Task task);
        Objects.Task FindById(Guid taskId);
        ICollection<KanbanState> GetKanbanStatesOfTask();
        Task UpdateAsyncKanbanState(Objects.Task task, TaskKanbanState s);
    }
}
