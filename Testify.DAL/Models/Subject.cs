﻿using System.ComponentModel.DataAnnotations;

namespace Testify.DAL.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public byte? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<ExamSchedule>? ExamSchedules { get; set; }
        public virtual ICollection<Exam>? Exams { get; set; }
    }
}
