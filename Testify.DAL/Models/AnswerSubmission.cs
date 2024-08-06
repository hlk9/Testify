using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class AnswerSubmission
    {
        public int Id { get; set; }
        public int SubmissionId { get; set; }
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
    }
}
