using System.ComponentModel.DataAnnotations;

namespace Testify.DAL.Models
{
    public class QuestionType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
}
