using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class DoingExam
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ExamId { get; set; }
        public int ExamScheduleId { get; set; }
        public string? Content { get; set; }  
    }
}
