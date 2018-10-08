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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(50), Required]
        public string ProjectEmail { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime? Deadline { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string ProjectManager { get; set; }

        public string Description { get; set; }
        public int Position { get; set; }
        private ICollection<UserRoles> users;
        public virtual ICollection<UserRoles> Users
        {
            get { return users; }
            set { Set(ref users, value); }
        }
        private ICollection<Column> column;
        public virtual ICollection<Column> Columns
        {
            get { return column; }
            set { Set(ref column, value); }
        }

        public virtual ICollection<ChatRoom> ChatRooms{ get; set; }

        public Project()
        {
            Id = Guid.NewGuid();
            Columns = new ObservableCollection<Column>();
            Users = new ObservableCollection<UserRoles>();
        }
    }
}
