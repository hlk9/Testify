using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.ViewModels
{
    public class ScoreStatistics
    {
        public Guid UserID { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int ExamId { get;set; }
        public string ExamName { get; set;}
        public int SubmissionId { get; set; }
        public double Score { get; set; }   
    }
}
