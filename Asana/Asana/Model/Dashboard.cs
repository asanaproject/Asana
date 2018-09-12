using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    [Table("Dashboard")]
    public class Dashboard
    {
        public Guid Id { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public Dashboard()
        {
            Id = Guid.NewGuid();
        }
    }
}
