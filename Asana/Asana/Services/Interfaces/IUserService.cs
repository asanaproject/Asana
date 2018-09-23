using Asana.Model;
using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Asana.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(User user);
        User Select(string email);
    }
}
