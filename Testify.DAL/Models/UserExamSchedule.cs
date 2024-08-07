using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class UserExamSchedule
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("ExamScheduleId")]
        public int ExamScheduleId { get; set; }
        public virtual ExamSchedule ExamSchedule { get; set; }
    }
}
