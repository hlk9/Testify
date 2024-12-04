using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.Web.Services
{
    public class SubmissionServices
    {
        HttpClient _httpClient;

        public SubmissionServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Submission>> GetAll()
        {
            var allSubmission = await _httpClient.GetAsync("submission/Get-All");
            var reponse = await allSubmission.Content.ReadFromJsonAsync<List<Submission>>();

            return reponse;
        }

        public async Task<Submission> GetById(int id)
        {
            var allSubmission = await _httpClient.GetAsync($"submission/Get-By-Id?id={id}");
            var reponse = await allSubmission.Content.ReadFromJsonAsync<Submission>();

            return reponse;
        }

        public async Task<Submission> CreateSubmission(Submission submission)
        {
            var obj = await _httpClient.PostAsJsonAsync("submission/Create-Submission", submission);
            var reponse = await obj.Content.ReadFromJsonAsync<Submission>();

            return reponse;
        }

        public async Task<int> NumberSubmit(Guid userId, int examscheduleId)
        {
            var numberofsubmit = await _httpClient.GetAsync($"submission/Check-NumberOfSubmit?userId={userId}&examscheduleId={examscheduleId}");
            var reponse = await numberofsubmit.Content.ReadFromJsonAsync<int>();

            return reponse;
        }

        public async Task<List<SubmissionWithName>> GetAllSubmittedByUser(Guid userId)
        {
            var lst = await _httpClient.GetAsync($"submission/Submitted-By-User?userId={userId}");
            var response = await lst.Content.ReadFromJsonAsync<List<SubmissionWithName>>();
            return response;
        }

        public async Task<List<Achievenment>> GetAllAchievenment(Guid userId)
        {
            var lst = await _httpClient.GetAsync($"submission/Achievenments?userId={userId}");
            var response = await lst.Content.ReadFromJsonAsync<List<Achievenment>>();
            return response;
        }

        public async Task<List<Submission>> GetHistory(Guid userId, int examscheduleId)
        {
            var lst = await _httpClient.GetAsync($"submission/Get-SubmitHistory?userId={userId}&examscheduleId={examscheduleId}");
            var response = await lst.Content.ReadFromJsonAsync<List<Submission>>();
            return response;
        }
    }
}
