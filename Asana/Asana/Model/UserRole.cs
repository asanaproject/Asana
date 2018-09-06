﻿using Asana.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    
    [Table("UserRole")]
    public class UserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,StringLength(25)]
        public string Type { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
