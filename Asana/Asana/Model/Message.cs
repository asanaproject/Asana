﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    [Table("Message")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ID { get; set; }
        public Message()
        {
            ID = Guid.NewGuid();
        }
        [Required, MaxLength(500)]
        public string Body { get; set; }

        [Required]
        public DateTime SendTime { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(ChatRoom))]
        public Guid ChatRoomId { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
