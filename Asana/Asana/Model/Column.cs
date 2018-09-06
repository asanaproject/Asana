using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Asana.Tools;

namespace Asana.Objects
{
    [Table("Columns")]
    public class Column:NotifyClass
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(25), Required]
        public string Title { get; set; } = "Column title";

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [NotMapped]
        private Visibility setting=Visibility.Visible;

        [NotMapped]
        public Visibility Setting
        {
            get { return setting; }
            set { OnPropertyChanged(nameof(Setting)); }
        }
        public virtual ICollection<Task> Tasks { get; set; }

        
    }
}
