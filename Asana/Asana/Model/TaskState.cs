using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    [Table("KanbanStates")]
    public class TaskState
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(25),Required]
        public string Name { get; set; }

        [StringLength(50),Required]
        public string Email { get; set; }
        
        [Required]
        public int DashboardId { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
        public int UserId { get; set; }
    }
}
