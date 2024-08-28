using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.Web.Services
{
    public class QuestionService
    {
        private readonly HttpClient _httpClient;

        private QuestionReposiroty _repoQuestion = new QuestionReposiroty();

        public QuestionService(HttpClient httpClient)
        {
            _httpClient = httpClient;   
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            return await _httpClient.GetFromJsonAsync<List<Question>>("Question/Get-All-Questions");
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Question>($"Question/Get-Question-By-Id?id={id}");
        }

        public async Task<Question> CreateQuestion(Question question)
        {
            var newQuestion = await _httpClient.PostAsJsonAsync("Question/Create-Question", question);
            var reponse = await newQuestion.Content.ReadFromJsonAsync<Question>();
            return reponse;
        }

        public async Task<Question> UpdateQuestion(Question question)
        {
            var updateQuestion = await _httpClient.PutAsJsonAsync("Question/Update-Question", question);
            var reponse = await updateQuestion.Content.ReadFromJsonAsync<Question>();
            return reponse;
        }

        public async Task<Question> UpdateStatus(int id, bool status)
        {
            return await _repoQuestion.UpdateStatusQuestion(id, status);
        }

        public async Task<Question> DeleteQuestion(int id)
        {
            var deleteQuestion = await _httpClient.DeleteAsync($"Question/Delete-Question?id={id}");
            var reponse = await deleteQuestion.Content.ReadFromJsonAsync<Question>();
            return reponse;
        }
    }
}
