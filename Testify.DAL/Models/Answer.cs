using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool? IsCorrect { get; set; }
        public bool? Status { get; set; }
        public int QuestionId { get; set; }
    }
}
