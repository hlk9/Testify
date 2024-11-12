﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Models;

namespace Testify.DAL.ViewModels
{
    public class QuestionInExam
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Code { get; set; }
        public byte? Status { get; set; }
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? DocumentPath { get; set; }
        public int QuestionTypeId { get; set; }
        public string? QuestionTypeName { get; set; }
        public int? QuestionLevelId { get; set; }
        public string? QuestionLeveName { get; set; }

        public bool IsChoosen { get; set; } = false; 
        

    }
}
