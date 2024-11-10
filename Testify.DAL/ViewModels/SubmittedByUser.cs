using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.ViewModels
{
    public class SubmittedByUser
    {
        public Guid UserId { get; set; }

        public int SubmissionId { get; set; }

        public int ExamScheduleId { get; set; }

        public string NameExam {  get; set; }

        public DateTime ExamDate { get; set; }

        public string SubjectName { get; set; }

        public double Score { get; set; }

        public string ClassName { get; set; }
    }
}
