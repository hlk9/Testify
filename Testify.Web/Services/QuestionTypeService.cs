using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.Web.Services
{
    public class QuestionTypeService
    {
        private readonly HttpClient _httpClient;

        public QuestionTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<QuestionType>> GetAllQuestionTypes()
        {
            return await _httpClient.GetFromJsonAsync<List<QuestionType>>("QuestionType/Get-All-Question-Type");
        }

        public async Task<QuestionType> GetQuestionTypesById(int id)
        {
            return await _httpClient.GetFromJsonAsync<QuestionType>($"QuestionType/Get-Question-Type-By-Id?id={id}");
        }

        public async Task<bool> CreateQuestionType(QuestionType questionType)
        {
            var statusCreate = await _httpClient.PostAsJsonAsync<QuestionType>("QuestionType/Create-Question-Type", questionType);
            if(statusCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateQuestionType(QuestionType questionType)
        {
            var statusUpdate = await _httpClient.PutAsJsonAsync<QuestionType>("QuestionType/Update-Question-Type", questionType);
            if (statusUpdate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteQuestionType(int id)
        {
            var statusDelete = await _httpClient.DeleteAsync($"QuestionType/Delete-Question-Type?id={id}");
            if (statusDelete.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
