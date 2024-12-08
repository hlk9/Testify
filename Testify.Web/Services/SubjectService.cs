using System.Web;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;
using Testify.Web.Components.Layout;

namespace Testify.Web.Services
{
    public class SubjectService
    {
        private readonly HttpClient _httpClient;
        public SubjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Subject>> GetAllSub(string? textSearch, bool isActive)
        {
            var allSubject = await _httpClient.GetAsync($"Subject/get-all-subject?keyWord={textSearch}&isActive={isActive}");
            var response = await allSubject.Content.ReadFromJsonAsync<List<Subject>>();

            return response;
        }

        public async Task<Subject> GetSubId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Subject>($"Subject/get-subject-by-id?id={id}");
        }

        public async Task<bool> CreateSub(Subject subject)
        {
            var status = await _httpClient.PostAsJsonAsync<Subject>("Subject/create-subject", subject);
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateSub(Subject subject)
        {
            var status = await _httpClient.PutAsJsonAsync<Subject>("Subject/update-subject", subject);
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<ErrorResponse> DeleteSub(int id)
        {
            var status = await _httpClient.DeleteAsync($"Subject/delete-subject?id={id}");

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

        public async Task<int> GetCountByUserId(Guid userId)
        {
            var count = await _httpClient.GetAsync($"Subject/Get-Count-By-UserId?userId={userId}");
            var response = await count.Content.ReadFromJsonAsync<int>();
            return response;
        }

        public async Task<ScoreDistribution> ScoreDistributionBySubject(int subjectId)
        {
            var lst = await _httpClient.GetAsync($"Subject/Score-Distribution-By-Subject?subjectId={subjectId}");
            var response = await lst.Content.ReadFromJsonAsync<ScoreDistribution>();
            return response;
        }

        public async Task<List<SubmissionViewModel>> GetAllBySubjectId(int? subjectId, string? textSearch, Guid? usersID, int? classId, DateTime? startTime, DateTime? endTime)
        {
            string startDateFormat = startTime?.ToString("yyyy/MM/dd");
            string endDateFormat = endTime?.ToString("yyyy/MM/dd");


            var lstUser = await _httpClient.GetAsync($"Subject/get-all-by-subjectId?subjectId={subjectId}&textSearch={textSearch}&usersID={usersID}&classId={classId}&startTime={HttpUtility.UrlEncode(startDateFormat)}&endTime={HttpUtility.UrlEncode(endDateFormat)}");
            var response = await lstUser.Content.ReadFromJsonAsync<List<SubmissionViewModel>>();
            return response;
        }


    }
}
