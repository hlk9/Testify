using System.ComponentModel.DataAnnotations;

namespace Testify.DAL.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        //[ForeignKey("OrganizationId")]
        //public int OrganizationId { get; set; }
        //public virtual Organization? Organization { get; set; }
        public byte? Status { get; set; }

        public virtual ICollection<ExamSchedule>? ExamSchedules { get; set; }
        public virtual ICollection<Exam>? Exams { get; set; }
    }
}
