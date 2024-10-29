using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Models;

namespace Testify.DAL.ViewModels
{
    public class QuestionInExcel
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public int QuestionLevelId { get; set; }
        public int QuestionTypeId { get; set; }
        public int SubjectId { get; set; }
        public string? ErorrMessage { get; set; }
        public bool? PassFail { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
