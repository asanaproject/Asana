using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public int LanguageId{ get; set; }
        public int CompanySize { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
