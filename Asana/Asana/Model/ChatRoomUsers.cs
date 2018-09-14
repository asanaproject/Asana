using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    [Table("ChatRoomUser")]
    public class ChatRoomUsers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(ChatRoom))]
        public int ChatRoomId { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
