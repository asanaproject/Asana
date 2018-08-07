using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    [Table("UserRoles")]
    public class UserRole
    {
        public int Id { get; set; }
        public enum Rules { Manager,User,Customer};
        public ICollection<User> Users { get; set; }
    }
}
