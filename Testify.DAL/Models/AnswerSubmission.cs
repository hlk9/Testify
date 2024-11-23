using System.ComponentModel.DataAnnotations.Schema;

namespace Testify.DAL.Models
{
    public class AnswerSubmission
    {
        public int Id { get; set; }
        [ForeignKey("SubmissionId")]
        public int SubmissionId { get; set; }
        [ForeignKey("AnswerId")]
        public int AnswerId { get; set; }
        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }

        public virtual Submission? Submission { get; set; }
        public virtual Answer? Answer { get; set; }
        public virtual Question? Question { get; set; }

    }


}
