using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class ExamActivityLog
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ExamId")]
        public int ExamId { get; set; }
        [ForeignKey("ExamDetailId")]
        public int? ExamDetailId { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public DateTime ActionTime { get; set; }
        public string? ActionType { get; set; }
        public virtual User? User { get; set; }
        public virtual Exam? Exam { get; set; }
        public virtual ExamDetail? ExamDetail { get; set; }
    }
}
