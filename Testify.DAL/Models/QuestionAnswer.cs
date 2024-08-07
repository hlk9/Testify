using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
