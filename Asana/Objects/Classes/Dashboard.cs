using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    public class Dashboard
    {
        public ICollection<Project> Projects { get; set; }
        public int Id { get; set; }
    }
}
