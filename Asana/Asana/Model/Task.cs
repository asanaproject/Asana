using GalaSoft.MvvmLight;
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
    public class Task:ViewModelBase
    {
        public Task()
        {
            Id = Guid.NewGuid();
            StarPath = "../Resources/Images/grey_star.png";
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [StringLength(25),Required]
        public string Title { get; set; }

        [ForeignKey("Column"),Required]
        public Guid ColumnId { get; set; }
        public virtual Column Column { get; set; }

        [ForeignKey("KanbanState")]
        public Guid KanbanStateId { get; set; }
        public virtual KanbanState KanbanState { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(ExtraInfo))]
        public Guid? ExtraInfoId { get; set; }
        public ExtraInfo ExtraInfo { get; set; }

        private DateTimeOffset createdAt;
        public DateTimeOffset CreatedAt
        {
            get { return createdAt; }
            set { Set(ref createdAt, value); }
        }
        private string starPath;

        public string StarPath
        {
            get { return starPath; }
            set { Set(ref starPath, value); }
        }


        public DateTime Deadline { get; set; }

        private bool isTaskAdded;

        public bool IsTaskAdded
        {
            get { return isTaskAdded; }
            set { Set(ref isTaskAdded,value); }
        }

        private bool isStarred;

        public bool IsStarred
        {
            get { return isStarred; }
            set { Set(ref isStarred,value); }
        }


    }
}
