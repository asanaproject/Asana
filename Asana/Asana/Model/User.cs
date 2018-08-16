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

namespace Asana.Objects
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20), Required]
        public string FirstName { get; set; }

        [StringLength(20), Required]
        public string LastName { get; set; }

        [StringLength(50), Required, RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$")]
        public string Email { get; set; }

        [StringLength(50), Required]
        public string CompanyName { get; set; }

        public byte[] Image { get; set; }

        [StringLength(20),RegularExpression(@"^ \+\d( ?\d){8,24}$")]
        public string PhoneNumber { get; set; }

        [Required,RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,18}$")]
        public string Password { get; set; }

        public int CompanySize { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
