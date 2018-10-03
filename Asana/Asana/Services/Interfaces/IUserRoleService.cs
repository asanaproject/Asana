using Asana.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task CreateAsync(UserRoles user);
        void LoadRoles(Guid projectId);
    }
   
}
