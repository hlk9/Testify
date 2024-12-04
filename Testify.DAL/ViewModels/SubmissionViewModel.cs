using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.ViewModels
{
    public class SubmissionViewModel
    {
        public string ExamName { get; set; }
        public string SubjectName { get; set; }
        public DateTime SubmitTime { get; set; }
        public bool Status { get; set; }
    }
}
