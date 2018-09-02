using Asana.Model;
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
        void Insert(User user);
        User Select(string email);
    }
}
