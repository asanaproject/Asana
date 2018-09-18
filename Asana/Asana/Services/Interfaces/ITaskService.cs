using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asana.Services.Interfaces
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task Add(Objects.Task task);
        System.Threading.Tasks.Task Update(Objects.Task task);

    }
}
