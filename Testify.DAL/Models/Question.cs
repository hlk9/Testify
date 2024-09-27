using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public byte? Status { get; set; }
        public int SubjectId { get; set; }
        public string? DocumentPath { get; set; }
        [ForeignKey("QuestionTypeId")]
        public int QuestionTypeId { get; set; }
         public virtual QuestionType? QuestionType { get; set; }
        [ForeignKey("QuestionLevelId")]
        public int QuestionLevelId { get; set; }
        public virtual QuestionLevel? QuestionLevel { get; set; }

        public virtual ICollection<QuestionAnswer>? QuestionAnswers { get; set; }
        public virtual ICollection<ExamDetailQuestion>? ExamDetailQuestions { get; set; }
    }
}
