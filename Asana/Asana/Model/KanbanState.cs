using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    [Table("KanbanState")]
    public class KanbanState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [StringLength(25),Required]
        public string Name { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public KanbanState()
        {
            Id = Guid.NewGuid();
        }
    }
}
