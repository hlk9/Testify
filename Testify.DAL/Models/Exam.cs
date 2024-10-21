using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("SubjectId")] 
        public int SubjectId  { get; set; }
        public virtual Subject? Subject { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfRepeat { get; set; } = 1;
        public byte Status { get; set; } = 1;
        public double MaximmumMark { get; set; }
        public double PassMark { get; set; }
        public int Duration { get; set; }
        [ForeignKey("ScoreMethodId")]
        public int? ScoreMethodId { get; set; }
        public virtual ScoreMethod? ScoreMethod { get; set; }
        public ICollection<ExamDetail>? ExamDetails { get; set; }
    }
}
