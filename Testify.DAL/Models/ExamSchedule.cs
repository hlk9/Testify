using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testify.DAL.Models
{
    public class ExamSchedule
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ExamId")]
        public int ExamId { get; set; }
        public ICollection<Exam>? Exams { get; set; }
        public string? Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Description { get; set; }
        public byte? Status { get; set; }
        [ForeignKey("SubjectId")]
        public int? SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
        [ForeignKey("RoomId")]
        public int? RoomId { get; set; }
        public virtual Room? Room { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

    }
}
