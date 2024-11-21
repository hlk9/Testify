using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testify.DAL.Models
{
    public class ClassExamSchedule
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ClassId")]
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }
        [ForeignKey("ExamScheduleId")]
        public int ExamScheduleId { get; set; }
        public virtual ExamSchedule? ExamSchedule { get; set; }
    }
}
