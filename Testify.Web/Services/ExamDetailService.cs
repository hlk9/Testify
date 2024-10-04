using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class ExamDetailService
    {
        private readonly HttpClient _httpClient;
        public ExamDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ExamDetail>> GetAllExamDetail()
        {
            var allExamDetail = await _httpClient.GetAsync("ExamDetail/Get-ExamDetail");
            var response = await allExamDetail.Content.ReadFromJsonAsync<List<ExamDetail>>();

            return response;
        }

        public async Task<ExamDetail> GetExamDetailById(int id)
        {
            return await _httpClient.GetFromJsonAsync<ExamDetail>($"ExamDetail/Get-ExamDetail-id?id={id}");
        }

        public async Task<List<ExamDetail>> GetExamDetailByExamId(int examId)
        {
            return await _httpClient.GetFromJsonAsync<List<ExamDetail>>($"ExamDetail/Get-ExamDetail-By-ExamID?examId={examId}");
        }
    }
}
