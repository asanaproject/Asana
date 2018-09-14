using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Services.Interfaces
{
    public interface IColumnService
    {
        System.Threading.Tasks.Task Add(Column column);
        System.Threading.Tasks.Task UpdateTitle(string title, Column column);
    }
}
