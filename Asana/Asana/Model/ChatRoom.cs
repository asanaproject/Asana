using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    [Table("ChatRoom")]
    public class ChatRoom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required,MaxLength(500)]
        public string Desc { get; set; }

        public ChatRoomType ChatRoomType { get; set; }

        public virtual ICollection<ChatRoomUsers> Users { get; set; }
    }
}
