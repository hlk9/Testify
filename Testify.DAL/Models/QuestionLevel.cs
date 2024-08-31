﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class QuestionLevel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; } 
        public virtual ICollection<Question>? Questions { get; set; }
    }
}
