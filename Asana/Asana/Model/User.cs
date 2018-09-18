    using Asana.Model;
using Asana.Objects;
using Asana.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        [Required]
        public string FullName { get; set; }
        
        [Required]
        public string  Username { get; set; }

        [Required, RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$")]
        public string Email { get; set; }
 
        public byte[] Image { get; set; }

        [Required]
        public string Password { get; set; }

        [NotMapped]
        public bool IsLogged { get; set; }

        public virtual ICollection<ChatRoomUsers> Users { get; set; }

        [Required,ForeignKey(nameof(UserRole))]
        public Guid ?UserRoleId { get; set; }
        public UserRole UserRole { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
