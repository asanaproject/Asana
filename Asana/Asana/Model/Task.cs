using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    [Table("Tasks")]
    public class Task
    {
        public int Id { get; set; }

        [StringLength(25),Required]
        public string Title { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public DateTime Deadline { get; set; }
        public int ColumnId { get; set; }
        public int ExtraInfoId { get; set; }
    }
}
