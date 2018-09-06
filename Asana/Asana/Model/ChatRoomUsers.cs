using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class ChatRoomUsers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [ForeignKey(nameof(ChatRoom))]
        public int ChatRoomId { get; set; }

        public virtual User User { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }

    }
}
