using Asana.Objects;
using Humanizer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Asana.Model
{
    public class TaskLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Task))]
        public Guid TaskId { get; set; }
        public virtual Task Task { get; set; }

        public string Message { get; set; }
        public string ChangedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public TaskLog()
        {
            Id = Guid.NewGuid();
        }
    }
}
