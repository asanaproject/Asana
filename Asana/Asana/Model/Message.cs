using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required, MaxLength(250)]
        public string Body { get; set; }

        [Required]
        public virtual int ChatUserID { get; set; }

        [Required]
        public DateTime Timestap { get; set; }

        public int ChatRoomId { get; set; }
        
    }
}
