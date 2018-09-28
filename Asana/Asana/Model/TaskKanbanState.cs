using Asana.Objects;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Asana.Model
{
    public class TaskKanbanState:ViewModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(KanbanState))]
        public Guid KanbanStateId { get; set; }
        public KanbanState KanbanState { get; set; }

        public string ChangedBy { get; set; }

        [ForeignKey(nameof(Task))]
        public Guid TaskId { get; set; }
        public Task Task { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
