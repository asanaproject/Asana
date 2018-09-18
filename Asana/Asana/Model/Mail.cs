using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    [Table("Mail")]
    public class Mail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ID { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(500)]
        public string Body { get; set; }

        [Required, MaxLength(100)]
        public string SenderEmail { get; set; }

        [Required]
        public DateTime SendTime { get; set; }

        [Required]
        public bool Marked { get; set; }

        [Required]
        public bool Favorite { get; set; }

        public byte[] BodyHtml { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Mail()
        {
            ID = Guid.NewGuid();
        }
    }
}
