using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    public class ExtraInfo
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Username { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
    }
}
