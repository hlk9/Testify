using Testify.DAL.Models;

namespace Testify.DAL.ViewModels
{
    public class QuestionWithAnswer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int QuestionType { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
