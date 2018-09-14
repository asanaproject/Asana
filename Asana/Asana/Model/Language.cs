﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    [Table("Language")]
    public class Language 
    {
        public Guid Id { get; set; }

        [StringLength(30),Required]
        public string Name { get; set; }

        public Language()
        {
            Id = new Guid();
        }
       
    }
}
