using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class ExamSchedule
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ExamId")]
        public int ExamId { get; set; }
        public ICollection<Exam>? Exams { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        [ForeignKey("SubjectId")]
        public int? SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
        [ForeignKey("RoomId")]
        public int RoomId { get; set; }
        public virtual Room? Room { get; set; }

    }
}
