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

        public async Task<ExamDetail> CreateExamDetail(ExamDetail examDetail)
        {
            var objNew = await _httpClient.PostAsJsonAsync("ExamDetail/Create-Exam-Detail", examDetail);
            var reponse = await objNew.Content.ReadFromJsonAsync<ExamDetail>();

            return reponse;
        }

        public async Task<bool> DeleteExamDetail(int id)
        {
            var status = await _httpClient.DeleteAsync($"ExamDetail/Delete-ExamDetail?id={id}");
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
