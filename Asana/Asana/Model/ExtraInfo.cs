using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    public class ExtraInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(20),Required]
        public string Username { get; set; }
        [StringLength(50),Required]
        public string Email { get; set; }
        [StringLength(20),Required]
        public string Password { get; set; }
    }
}
