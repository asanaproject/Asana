using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Services.Interfaces
{
     public interface IProjectService
    {
       Task Add(Objects.Project project);
    }
}
