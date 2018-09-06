using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    [Table("Task")]
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(25),Required]
        public string Title { get; set; }

        [ForeignKey("Column"),Required]
        public int ColumnId { get; set; }
        public virtual Column Column { get; set; }

        [ForeignKey("KanbanState")]
        public int KanbanStateId { get; set; }
        public virtual KanbanState KanbanState { get; set; }

        public DateTime Deadline { get; set; }
    }
}
