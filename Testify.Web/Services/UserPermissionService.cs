using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class UserPermissionService
    {
        HttpClient _httpClient;
        public UserPermissionService( HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserPermission>> GetAllByUserId(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<List<UserPermission>>("UserPermission/Get-PermissionByUserId?id="+id);
        }

        public async Task<bool> DeleteUPermission(int id)
        {
            var stats = await _httpClient.DeleteAsync("UserPermission/Delete-UserPermission?id=" + id);
            return stats.IsSuccessStatusCode;
        }


        public async Task<bool> AddAsync(UserPermission userPermission)
        {
            var stats = await _httpClient.PostAsJsonAsync("UserPermission/Create-UserPermission", userPermission);
            return stats.IsSuccessStatusCode;
        }

        public async Task<bool> CheckHasPermission(Guid userId, string permission)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"UserPermission/Check-PermissionByUserIdAndName?userId={userId}&permission={permission}");
        }

    }
}
