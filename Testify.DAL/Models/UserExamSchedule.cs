using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class UserExamSchedule
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ExamScheduleId { get; set; }
    }
}
