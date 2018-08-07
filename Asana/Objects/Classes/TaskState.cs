using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    public class TaskState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectEmail { get; set; }
        public int DashboardId { get; set; }
        public ICollection<Column> Columns { get; set; }
        public int UserId { get; set; }
    }
}
