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
        Task UpdateAsync(Objects.Task task);
        ICollection<KanbanState> GetKanbanStatesOfTask();
        Task UpdateAsyncKanbanState(Objects.Task task, TaskKanbanState s);

    }
}
