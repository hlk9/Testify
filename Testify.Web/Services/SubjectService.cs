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

        public async Task<bool> DeleteSub(int id)
        {
            var status = await _httpClient.DeleteAsync($"Subject/delete-subject?id={id}");
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<int> GetCountByUserId(Guid userId)
        {
            var count = await _httpClient.GetAsync($"Subject/Get-Count-By-UserId?userId={userId}");
            var response = await count.Content.ReadFromJsonAsync<int>();
            return response;
        }
    }
}
