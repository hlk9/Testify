using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class ExamService
    {
        private readonly HttpClient _httpClient;
        public ExamService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Exam>> GetAllExam(string? textSearch, bool isActive)
        {
            var allExam = await _httpClient.GetAsync($"Exam/Get-Exams?KeyWord={textSearch}&isActive={isActive}");
            var reponse = await allExam.Content.ReadFromJsonAsync<List<Exam>>();
            return reponse;
        }

        public async Task<Exam> GetExamId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Exam>($"Exam/get-exams-by-id={id}");
        }

        public async Task<Exam> CreateExam(Exam e)
        {
            var newExam = await _httpClient.PostAsJsonAsync("Exam/Add-Exam", e);
            var reponse = await newExam.Content.ReadFromJsonAsync<Exam>();
            return reponse;
        }

        public async Task<Exam> UpdateExam(Exam e)
        {
            var updateExam = await _httpClient.PutAsJsonAsync("Exam/Update-Exam", e);
            var reponse = await updateExam.Content.ReadFromJsonAsync<Exam>();
            return reponse;
        }

        public async Task<bool> DeleteExam(int id)
        {
            var status = await _httpClient.DeleteAsync($"Exam/Delete-Exam?id={id}");
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
