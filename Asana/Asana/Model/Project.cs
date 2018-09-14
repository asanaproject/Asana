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
    [Table("Project")]
    public class Project : ViewModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [StringLength(50), Required]
        public string ProjectEmail { get; set; }

        [ForeignKey("Dashboard"), Required]
        public Guid DashboardId { get; set; }
        public virtual Dashboard Dashboard { get; set; }

        public virtual ICollection<UsersProjects> Users { get; set; }
        private ICollection<Column> column;

        public virtual ICollection<Column> Columns
        {
            get { return column; }
            set { Set(ref column, value); }
        }

        public Project()
        {
            Id = Guid.NewGuid();
            Columns = new ObservableCollection<Column>();
        }
    }
}
