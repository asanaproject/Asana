using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asana.Model;
using Asana.Objects;

namespace Asana.Services.Interfaces
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task Add(Task task);
    }
}
