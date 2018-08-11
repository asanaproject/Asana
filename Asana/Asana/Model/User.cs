using System;
using System.Collections.Generic;
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

        [StringLength(20),Required]
        public string FirstName { get; set; }

        [StringLength(20),Required]
        public string LastName { get; set; }

        [StringLength(50),Required]
        public string Email { get; set; }

        [StringLength(50),Required]
        public string CompanyName { get; set; }


        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public int LanguageId{ get; set; }
        public int CompanySize { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
