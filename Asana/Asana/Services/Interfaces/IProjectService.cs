using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Services.Interfaces
{
    public interface IProjectService
    {
        System.Threading.Tasks.Task CreateAsync(Project project);
        System.Threading.Tasks.Task RemoveAsync(Project project);
        System.Threading.Tasks.Task UpdateAsync(Project project);
        ICollection<Project> GetAll(Guid userId );

    }
}
