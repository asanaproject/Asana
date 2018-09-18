using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Services.Interfaces
{
    public interface IExtraInfoService
    {
        System.Threading.Tasks.Task Add(ExtraInfo extraInfo);
        bool FindByEmail(string email);


    }
}
