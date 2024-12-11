using Testify.DAL.Models;
using Testify.DAL.ViewModels;
using Testify.Web.Components.Pages.Examiner.Dialog.DeThi;

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

        public async Task<ErrorResponse> DeleteExamDetail(int id)
        {
            var response = await _httpClient.DeleteAsync($"ExamDetail/Delete-ExamDetail?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return new ErrorResponse { Success = true };
            }

            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return new ErrorResponse
            {
                Success = false,
                ErrorCode = error?.ErrorCode ?? "UNKNOWN_ERROR",
                Message = error?.Message ?? "UNKNOWN_ERROR"
            };
        }

        

        public async Task<bool> UpdateStatusExamDetail(ExamDetail e)
        {
            var updateExamDetail = await _httpClient.PutAsJsonAsync("ExamDetail/Update-satus", e);
            if (updateExamDetail.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsExamDetailCodeDuplicate(string code, int? idSub)
        {
            var response = await _httpClient.GetAsync($"ExamDetail/CheckTrungCodeDT?code={code}&idSub={idSub}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return bool.TryParse(content, out var result) && result;
            }
            else
            {
                return false;
            }
        }

    }
}
