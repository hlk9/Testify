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
    }
}
