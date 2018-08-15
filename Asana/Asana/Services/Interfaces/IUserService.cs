using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Services.Interfaces
{
    public interface IUserService
    {
        System.Threading.Tasks.Task Insert(User user);
    }
}
