using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    [Table("Customer")]
    public class ExtraInfo
    {
        public Guid Id { get; set; }

        [StringLength(20),Required]
        public string Username { get; set; }

        [StringLength(50),Required]
        public string Email { get; set; }

        [StringLength(20),Required]
        public string Password { get; set; }

        public ExtraInfo()
        {
            Id = new Guid();
        }
    }
}
