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
        public Task Task { get; set; }

        [NotMapped]
        private readonly System.Timers.Timer timer;
        [NotMapped]
        public string CreationDate { get; set; }

        public string Message { get; set; }
        public string ChangedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }    
        public bool TitleIsChanged { get; set; }
        public bool ImageIsChanged { get; set; }
        public bool CurrentKanbanStateIsChanged { get; set; }
        public bool DescriptionIsChanged { get; set; }
        public bool ExtraInfoIsChanged { get; set; }
        public bool IsAdded { get; set; }
        public bool DeadlineIsChanged { get; set; }
        public bool StarredIsChanged { get; set; }

        public TaskLog()
        {
            Id = Guid.NewGuid();

            timer = new System.Timers.Timer(1000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CreationDate ="-" + CreatedAt.Humanize();

        }
    }
}
