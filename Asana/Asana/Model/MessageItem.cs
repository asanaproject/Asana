using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    [Table("MessageItem")]
    public class MessageItem
    {
        public string ProfName { get; set; }
        public string Body { get; set; }
        public bool ECS { get; set; }
    }
}
