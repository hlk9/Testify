using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.ViewModels
{
    public class ClassesWithLecturer
    {
        public Guid UserID { get; set; }
        public int ClassID { get; set; }
        public Guid LecturerID { get; set; }
        public string LecturerName { get; set; }
        public string ClassName { get; set; }
        public int SubmissionId { get; set; }
        public double Score { get; set; }
    }
}
