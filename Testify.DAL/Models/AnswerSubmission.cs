using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
