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

        public async Task<ExamDetailQuestion> CreateExamDetailQuestion(ExamDetailQuestion examDetailQuestion)
        {
            var objNew = await _httpClient.PostAsJsonAsync("ExamDetailQuestion/Create", examDetailQuestion);
            var reponse = await objNew.Content.ReadFromJsonAsync<ExamDetailQuestion>();
            return reponse;
        }

        public async Task<bool> DeleteExamDetailQuestionsByExamDetailId(int idExamDetail)
        {
            var status = await _httpClient.DeleteAsync($"ExamDetailQuestion/Delete?idExamDetail={idExamDetail}");
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
