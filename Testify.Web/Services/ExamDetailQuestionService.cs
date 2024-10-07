using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class ExamDetailQuestionService
    {
        private readonly HttpClient _httpClient;
        public ExamDetailQuestionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ExamDetailQuestion>> GetAllByExamDetailId(int examDetailId)
        {
            return await _httpClient.GetFromJsonAsync<List<ExamDetailQuestion>>($"ExamDetailQuestion/Get-ExamDetailQuestion-By-ExamDetailID?examDetailID={examDetailId}");
        }
    }
}
