using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    [Table("Projects")]
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string ProjectEmail { get; set; }

        [ForeignKey("Dashboard")]
        public int DashboardId { get; set; }
        public virtual Dashboard Dashboard { get; set; }

        public ICollection<Column> Columns { get; set; }

        public int UserId { get; set; }
    }
}
