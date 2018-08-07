using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    [Table("Dashboards")]
    public class Dashboard
    {
        public ICollection<Project> Projects { get; set; }
        public int Id { get; set; }
    }
}
