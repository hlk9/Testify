using Testify.DAL.Models;

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
    }
}
