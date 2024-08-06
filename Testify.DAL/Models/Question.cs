﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? Status { get; set; }
        public int SubjectId { get; set; }
        public string DocumentURL { get; set; }
        public int QuestionTypeId { get; set; }
        public int QuestionLevelId { get; set; }
    }
}
