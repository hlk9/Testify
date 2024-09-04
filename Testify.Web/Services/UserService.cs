using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<User>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("User/Get-All-User");
        }
        public async Task<User> GetByIdUser(int id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"User/Get-By-Id-User?id={id}");
        }

        public async Task<bool> Create(User user)
        {
            var result = await _httpClient.PostAsJsonAsync<User>("User/Create-User", user);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Update(User user)
        {
            var result = await _httpClient.PutAsJsonAsync<User>("User/Update-User", user);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync($"User/Delete-User?id={id}");
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
