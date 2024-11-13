using System.ComponentModel.DataAnnotations;

namespace Testify.DAL.Models
{
    public class ScoreMethod
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte? Status { get; set; }
        public virtual ICollection<Exam>? Exams { get; set; }
    }
}
