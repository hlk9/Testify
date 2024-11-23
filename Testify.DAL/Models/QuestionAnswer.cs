using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testify.DAL.Models
{
    public class QuestionAnswer
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        [ForeignKey("AnswerId")]
        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
    }
}
