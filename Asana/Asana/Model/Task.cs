using Asana.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            KanbanStates = new ObservableCollection<KanbanState>();
            TaskLogs = new ObservableCollection<TaskLog>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [StringLength(25),Required]
        public string Title { get; set; }

        [ForeignKey("Column"),Required]
        public Guid ColumnId { get; set; }
        public virtual Column Column { get; set; }

        public byte[] Image { get; set; }
        [NotMapped]
        public string ImagePath { get; set; }

        public string AssignedTo { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        [NotMapped]
        private ICollection<KanbanState> kanbanStates;
        public ICollection<KanbanState> KanbanStates
        {
            get { return kanbanStates; }
            set { Set(ref kanbanStates,value); }
        }

        private  ICollection<TaskKanbanState> taskKanbanStates;
        public ICollection<TaskKanbanState> TaskKanbanStates
        {
            get { return taskKanbanStates; }
            set { Set(ref taskKanbanStates, value); }
        }


        private KanbanState currentKanbanState;
        public KanbanState CurrentKanbanState
        {
            get { return currentKanbanState; }
            set {Set(ref currentKanbanState,value); }
        }


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

        private bool isTaskAdded;
        public bool IsTaskAdded
        {
            get { return isTaskAdded; }
            set { Set(ref isTaskAdded,value); }
        }

        private ICollection<TaskLog> taskLogs;
        public virtual ICollection<TaskLog> TaskLogs 
        {
            get { return taskLogs; }
            set {Set(ref taskLogs,value); }
        }


        private bool isStarred;
        public bool IsStarred
        {
            get { return isStarred; }
            set { Set(ref isStarred,value); }
        }


    }
}
