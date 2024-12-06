using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testify.DAL.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("SubjectId")]
        public int SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfRepeat { get; set; } = 1;
        public byte Status { get; set; } = 1;
        public double MaximmumMark { get; set; }
        public double PassMark { get; set; }
        public bool? AllowViewResult { get; set; } = true;
        public int Duration { get; set; }
        [ForeignKey("ScoreMethodId")]
        public int? ScoreMethodId { get; set; }
        public virtual ScoreMethod? ScoreMethod { get; set; }
        public ICollection<ExamDetail>? ExamDetails { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
