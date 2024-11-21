using Testify.DAL.Models;

namespace Testify.DAL.ViewModels
{
    public class AnswerAndQuestion
    {
        public int QuestionId { get; set; }

        public string ContentQuestion { get; set; }

        public int QuestionTypeId { get; set; }

        public double PointOfQuestion { get; set; }

        public List<Answer> LstAnswer { get; set; }
    }
}
