    using Asana.Model;
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
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


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

        public ICollection<ChatRoomUsers> Users { get; set; }
        //   public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
