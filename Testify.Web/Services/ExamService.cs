using Testify.API.DTOs;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

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

        public async Task<ErrorResponse> DeleteExam(int id)
        {
            var status = await _httpClient.PutAsync($"Exam/Delete-Exam?id={id}", null);

            if (status.IsSuccessStatusCode)
            {
                return new ErrorResponse { Success = true };
            }

            var error = await status.Content.ReadFromJsonAsync<ErrorResponse>();
            return new ErrorResponse
            {
                Success = false,
                ErrorCode = error?.ErrorCode ?? "UNKNOWN_ERROR",
                Message = error?.Message ?? "UNKNOWN_ERROR"
            };
        }
        public async Task<List<Exam>> GetListOfSubject(int id)
        {
            var lst = await _httpClient.GetFromJsonAsync<List<Exam>>("Exam/Get-ExamBySubject?id=" + id);
            return lst;
        }

        public async Task<List<ExamWhitQusetion>> GetInforBasic(string? textSearch)
        {
            var lst = await _httpClient.GetFromJsonAsync<List<ExamWhitQusetion>>($"Exam/Get-InfoBasic?textSearch={textSearch}");
            return lst;
        }

        public async Task<int> GetCountExamByUserId(Guid userId)
        {
            var count = await _httpClient.GetAsync($"Exam/Get-Count-Exam-By-UserId?userId={userId}");
            var response = await count.Content.ReadFromJsonAsync<int>();
            return response;
        }

        public async Task<List<Exam>> GetExamsByUserId(Guid UserId)
        {
            var count = await _httpClient.GetAsync($"Exam/Get-Exams-By-UserId?UserId={UserId}");
            var response = await count.Content.ReadFromJsonAsync<List<Exam>>();
            return response;
        }

        public async Task<ScoreDistribution> ScoreDistributionByExam(int ExamId)
        {
            var lst = await _httpClient.GetAsync($"Exam/Score-Distribution-By-Exam?ExamId={ExamId}");
            var response = await lst.Content.ReadFromJsonAsync<ScoreDistribution>();
            return response;
        }

        public async Task<bool> IsExamCodeDuplicate_Exam(string name, int? idSub)
        {
            //var response = await _httpClient.GetAsync($"Exam/Check-TrungNamExam?code={name}");
            var res = await _httpClient.GetAsync($"Exam/Check-TrungNamExam?name={name}&idSub={idSub}");   
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();

                return bool.TryParse(content, out var result) && result;
            }
            else
            {
                return false;
            }
        }
    }
}
