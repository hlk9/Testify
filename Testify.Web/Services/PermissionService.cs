using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class PermissionService
    {
        HttpClient _httpClient;
        public PermissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Permission>> GetAllPermissions()
        {
            var response =  await _httpClient.GetFromJsonAsync<List<Permission>>("Permission/GetAll");
            return response;
        }
    }
}
