using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class ExamSchedule
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public int SubjectId { get; set; }
        public int RoomId { get; set; }

    }
}
