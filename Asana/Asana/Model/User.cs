    using Asana.Model;
using Asana.Objects;
using Asana.Tools;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    [Table("User")]
    public class User:ViewModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }
        
        [Required]
        public string  Username { get; set; }

        [Required, RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [NotMapped]
        public bool IsLogged { get; set; }

        public byte[] Image { get; set; }

        private ICollection<Project> projects;
        public ICollection<Project> Projects
        {
            get { return projects; }
            set {Set(ref projects ,value); }
        }

        public virtual ICollection<ChatRoomUsers> Users { get; set; }
        public User()
        {
            Id = Guid.NewGuid();
            Projects = new ObservableCollection<Project>();
        }
    }
}
