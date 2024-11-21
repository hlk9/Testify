using System.ComponentModel.DataAnnotations;

namespace Testify.DAL.Models
{
    public class QuestionLevel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }
    }
}
