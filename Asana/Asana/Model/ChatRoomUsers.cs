using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    [Table("ChatRoomUser")]
    public class ChatRoomUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(ChatRoom))]
        public Guid ChatRoomId { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }

        public ChatRoomUsers()
        {
            Id = Guid.NewGuid();
        }
    }
}
