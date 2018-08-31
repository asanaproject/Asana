using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class ChatRoomMsg
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public virtual ICollection<ChatRoom> ChatRooms { get; set; }

        public virtual ICollection<Message> messages { get; set; }
    }
}
