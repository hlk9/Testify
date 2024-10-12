using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        [ForeignKey(nameof(ExamDetailId))]
        public int ExamDetailId { get; set; }
        public virtual ExamDetail? ExamDetail { get; set; }
        [ForeignKey("ExamScheduleId")]
        public int ExamScheduleId { get; set; }
        public virtual ExamSchedule? ExamSchedule { get; set; }
        public DateTime SubmitTime { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public double TotalMark { get; set; }
        public bool IsPassed { get; set; }
        public int UnAnswered { get; set; }
        public int Answered { get; set; }
        public string Note { get; set; }
        //[ForeignKey("OrganizationId")]
        //public int OrganizationId { get; set; }
        //public virtual Organization Organization { get; set; }

        public bool? Status { get; set; }

        public virtual ICollection<AnswerSubmission>? AnswerSubmissions { get; set; }

    }
}
