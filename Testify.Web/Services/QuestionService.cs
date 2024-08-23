using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class QuestionService
    {
        private readonly HttpClient _httpClient;

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

        public async Task<bool> CreateQuestion(Question question)
        {
            var statusCreate = await _httpClient.PostAsJsonAsync<Question>("Question/Create-Question", question);
            if(statusCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateQuestion(Question question)
        {
            var statusUpdate = await _httpClient.PutAsJsonAsync<Question>("Question/Update-Question", question);
            if (statusUpdate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteQuestion(int id)
        {
            var statusDelete = await _httpClient.DeleteAsync($"Question/Delete-Question?id={id}");
            if (statusDelete.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
