﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Asana.Tools;
using Asana.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Humanizer;

namespace Asana.Objects
{
    [Table("Column")]
    public class Column : ViewModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public bool IsColumnAdded { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public int Position { get; set; }
        private ICollection<Task> tasks;
        public virtual ICollection<Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        public Column()
        {
            Id = Guid.NewGuid();
            Tasks = new ObservableCollection<Task>();           
        }
    }
}
