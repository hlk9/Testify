using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public byte Status { get; set; }
    } 
}
