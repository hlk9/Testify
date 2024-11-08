using System.Net.Http.Json;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.Web.Services
{
    public class ClassUserServices
    {
        private readonly HttpClient _httpClient;

        public ClassUserServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClassUser>> GetAllClassUser(byte Status)
        {
            var all = await _httpClient.GetAsync($"ClassUser/Get-All?Status={Status}");
            var reponse = await all.Content.ReadFromJsonAsync<List<ClassUser>>();
            return reponse;
        }

        public async Task<List<ClassWithClassUser>> GetAll(string id, string? search, byte Status)
        {
            var all = await _httpClient.GetAsync($"ClassUser/Get-By-StudentId?StudentId={id}&Search={search}&Status={Status}");
            var reponse = await all.Content.ReadFromJsonAsync<List<ClassWithClassUser>>();
            return reponse;
        }

        public async Task<ClassUser> CreateClassUser(ClassUser classUser)
        {
            var obj = await _httpClient.PostAsJsonAsync("ClassUser/Create-ClassUser", classUser);
            var reponse = await obj.Content.ReadFromJsonAsync<ClassUser>();
            return reponse;
        }

        public async Task<ClassUser> UpdateStatus(ClassUser classUser)
        {
            var obj = await _httpClient.PutAsJsonAsync($"ClassUser/Update-Status",classUser);
            var reponse = await obj.Content.ReadFromJsonAsync<ClassUser>();
            return reponse;
        }

        //public async Task<ClassUser> RefuseUser(ClassUser classUser)
        //{
        //    var obj = await _httpClient.PutAsJsonAsync($"ClassUser/Refuse-User", classUser);
        //    var reponse = await obj.Content.ReadFromJsonAsync<ClassUser>();
        //    return reponse;
        //}

        public async Task<bool> DeleteUserInClass(Guid id, int classId)
        {
            var obj = await _httpClient.DeleteAsync($"ClassUser/Delete-User-In-Class?id={id}&&classId={classId}");
            if(obj.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
