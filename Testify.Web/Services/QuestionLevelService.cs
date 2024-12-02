using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.Web.Services
{
    public class QuestionLevelService
    {
        private readonly HttpClient _httpClient;

        public QuestionLevelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<QuestionLevel>> GetAllQuestionLevels()
        {
            return await _httpClient.GetFromJsonAsync<List<QuestionLevel>>("QuestionLevel/Get-All-Question-Level");
        }

        public async Task<QuestionLevel> GetQuestionLevelById(int id)
        {
            return await _httpClient.GetFromJsonAsync<QuestionLevel>($"QuestionLevel/Get-Question-Level-By-Id?id={id}");
        }

        public async Task<bool> CreateQuestionLevel(QuestionLevel questionLevel)
        {
            var statusCreate = await _httpClient.PostAsJsonAsync<QuestionLevel>("QuestionLevel/Create-Question-Level", questionLevel);
            if (statusCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateQuestionLevel(QuestionLevel questionLevel)
        {
            var statusUpdate = await _httpClient.PutAsJsonAsync<QuestionLevel>("QuestionLevel/Update-Question-Level", questionLevel);
            if (statusUpdate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<ErrorResponse> DeleteQuestionLevel(int id)
        {
            var statusDelete = await _httpClient.DeleteAsync($"QuestionLevel/Delete-Question-Level?id={id}");

            if (statusDelete.IsSuccessStatusCode)
            {
                return new ErrorResponse { Success = true };
            }

            var error = await statusDelete.Content.ReadFromJsonAsync<ErrorResponse>();
            return new ErrorResponse
            {
                Success = false,
                ErrorCode = error?.ErrorCode ?? "UNKNOWN_ERROR",
                Message = error?.Message ?? "UNKNOWN_ERROR"
            };
        }
    }
}
