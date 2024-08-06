using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class ExamDetailQuestion
    {
        public int Id { get; set; }
        public int ExamDetailId { get; set; }
        public int QuestionId { get; set; }     
        public double Point { get; set; }
    }
} 
