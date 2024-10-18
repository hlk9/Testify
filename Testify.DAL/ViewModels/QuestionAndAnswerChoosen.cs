using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Models;

namespace Testify.DAL.ViewModels
{
    public class QuestionAndAnswerChoosen
    {
        public int QuestionId { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
