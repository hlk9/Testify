using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class LevelService
    {
        private readonly HttpClient _httpClient;
        public LevelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Level>> GetLevelId(int id)
        {
            var allLevel = await _httpClient.GetAsync($"Level/get-all-level-by-id?id{id}");
            var reponse = await allLevel.Content.ReadFromJsonAsync<List<Level>>();
            return reponse;
        }

        public async Task<List<Level>> GetAllLevel()
        {
            var allLevel = await _httpClient.GetAsync($"Level/get-all");
            var reponse = await allLevel.Content.ReadFromJsonAsync<List<Level>>();
            return reponse;
        }
    }
}
