using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class PermissionService
    {
        private readonly HttpClient _httpClient;
        public PermissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Permission>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<Permission>>("Permission/Get-All-Permission");
        }
        public async Task<Permission> GetByIdPermission(int id)
        {
            return await _httpClient.GetFromJsonAsync<Permission>($"Permission/Get-By-Id-Permission?id={id}");
        }

        public async Task<bool> Create(Permission permission)
        {
            var result = await _httpClient.PostAsJsonAsync<Permission>("User/Create-Permission", permission);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Update(Permission permission)
        {
            var result = await _httpClient.PutAsJsonAsync<Permission>("Permission/Update-Permission", permission);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync($"Permission/Delete-Permission?id={id}");
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
