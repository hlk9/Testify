using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class Submission
    {
        public Guid UserId { get; set; }
        public int ExamDetailId { get; set; }
        public int ExamScheduleId { get; set; }
        public DateTime SubmitTime { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public double TotalMark { get; set; }
        public bool IsPassed { get; set; }
        public int UnAnswered { get; set; }
        public int Answered { get; set; }
        public string Note { get; set; } 
        public int OrganizationId { get; set; }
        public bool Status { get; set; }

    }
}
