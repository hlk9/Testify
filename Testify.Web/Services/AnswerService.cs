using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class AnswerService
    {
        private readonly HttpClient _httpClient;

        public AnswerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Answer>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<Answer>>("Answer/Get-All-Answers");
        }

        public async Task<Answer> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Answer>($"Answer/Get-Answer-By-Id?id={id}");
        }

        public async Task<bool> Create(Answer answer)
        {
            var result = await _httpClient.PostAsJsonAsync<Answer>("Answer/Create-Answer", answer);
            if(result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Update(Answer answer)
        {
            var result = await _httpClient.PutAsJsonAsync<Answer>("Answer/Update-Answer", answer);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync($"Answer/Delete-Answer?id={id}");
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
