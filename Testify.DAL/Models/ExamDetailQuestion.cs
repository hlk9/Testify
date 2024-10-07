using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class ExamDetailQuestion
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ExamDetailId")]
        public int ExamDetailId { get; set; }
        public virtual ExamDetail ExamDetail { get; set; }
        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }     
        public virtual Question Question { get; set; }
        public double Point { get; set; }
    }
} 
