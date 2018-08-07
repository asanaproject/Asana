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
        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public int CountryId { get; set; }
        public int LanguageId{ get; set; }
        public int CompanySize { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
