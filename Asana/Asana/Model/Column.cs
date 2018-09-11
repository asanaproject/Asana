using System;
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

namespace Asana.Objects
{
    [Table("Column")]
    public class Column:ViewModelBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(25), Required]
        public string Title { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        private ICollection<Task> tasks;
        public virtual ICollection<Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }
        public bool IsColumnAdded { get; set; }

        public Column()
        {
            Tasks = new ObservableCollection<Task>();
        }
    }
}
