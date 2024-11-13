using Testify.API.DTOs;
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

        public async Task<List<Exam>> GetAllActive()
        {
            var allExam = await _httpClient.GetAsync($"Exam/Get-Active");
            var reponse = await allExam.Content.ReadFromJsonAsync<List<Exam>>();
            return reponse;
        }

        public async Task<Exam> GetExamId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Exam>($"Exam/get-exams-by-id?id={id}");
        }

        public async Task<Exam> CreateExam(Exam e)
        {
            var newExam = await _httpClient.PostAsJsonAsync("Exam/Add-Exam", e);
            var reponse = await newExam.Content.ReadFromJsonAsync<Exam>();
            return reponse;
        }

        public async Task<bool> UpdateExam(Exam e)
        {
            var updateExam = await _httpClient.PutAsJsonAsync("Exam/Update-Exam", e);
            //var reponse = await updateExam.Content.ReadFromJsonAsync<Exam>();
            //return reponse;
            if (updateExam.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public async Task<List<Exam>> GetListOfSubject(int id)
        {
            var lst = await _httpClient.GetFromJsonAsync<List<Exam>>("Exam/Get-ExamBySubject?id=" + id);
            return lst;
        }

        public async Task<List<ExamWhitQusetion>> GetInforBasic()
        {
            var lst = await _httpClient.GetFromJsonAsync<List<ExamWhitQusetion>>("Exam/Get-InfoBasic");
            return lst;
        }



    }
}
