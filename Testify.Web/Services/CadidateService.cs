using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class CadidateService
    {
        private readonly HttpClient _httpClient;
        public CadidateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetAllCandidate()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("Candidate/Get-All-Candidate");
        }

        public async Task<User> GetCandidateById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"Candidate/Get-Candidate-By-Id?id={id}");
        }

        public async Task<bool> CreateCandidate(User user)
        {
            var statusCreate = await _httpClient.PostAsJsonAsync<User>("Candidate/Create-Candidate", user);
            if (statusCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCandidate(User user)
        {
            var statusUpdate = await _httpClient.PutAsJsonAsync<User>("Candidate/Update-Candidate", user);
            if (statusUpdate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCandiadate(Guid id)
        {
            var statusDelete = await _httpClient.DeleteAsync($"Candidate/Delete-Candidate?id={id}");
            if (statusDelete.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
