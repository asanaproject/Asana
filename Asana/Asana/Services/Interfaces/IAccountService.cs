using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Services.Interfaces
{
    interface IAccountService
    {
      bool  LoginControl(string Email,string Password);
    }
}
