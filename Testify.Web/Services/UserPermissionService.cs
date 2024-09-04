using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class UserPermissionService
    {
        private readonly HttpClient _httpClient;
        public UserPermissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<UserPermission>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<UserPermission>>("UserPermission/Get-All-UserPermission");
        }
        public async Task<UserPermission> GetByIdUserPermission(int id)
        {
            return await _httpClient.GetFromJsonAsync<UserPermission>($"UserPermission/Get-By-Id-UserPermission?id={id}");
        }

        public async Task<bool> Create(UserPermission userPermission)
        {
            var result = await _httpClient.PostAsJsonAsync<UserPermission>("User/Create-UserPermission", userPermission);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Update(UserPermission userPermission)
        {
            var result = await _httpClient.PutAsJsonAsync<UserPermission>("UserPermission/Update-UserPermission", userPermission);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync($"UserPermission/Delete-UserPermission?id={id}");
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
