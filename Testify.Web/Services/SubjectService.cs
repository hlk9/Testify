using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class SubjectService
    {
        private readonly HttpClient _httpClient;
        public SubjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Subject>> GetAllSub()
        {
            return await _httpClient.GetFromJsonAsync<List<Subject>>("Subject/get-all-subject");
        }

        public async Task<Subject> GetSubId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Subject>($"Subject/get-subject-id?id={id}");
        }

        public async Task<bool> CreateSub(Subject subject)
        {
            var status = await _httpClient.PostAsJsonAsync<Subject>("Subject/create-subject",subject);
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

        public async Task<bool> DeleteSub(int id)
        {
            var status = await _httpClient.DeleteAsync($"Subject/delete-subject?id={id}");
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
