using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.ViewModels
{
    public class QuestionInExcel
    {
        public int connectionId { get; set; }
        public string Content { get; set; }
        public int QuestionLevelId { get; set; }
        public int QuestionTypeId { get; set; }
        public int SubjectId { get; set; }
        public string? ErorrMessage { get; set; }

        public bool? PassFail { get; set; }
        public List<AnswerInExcel> Answers { get; set; }
    }
}
