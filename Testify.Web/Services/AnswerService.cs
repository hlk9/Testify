using Testify.DAL.Models;
using Testify.DAL.ViewModels;

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

        public async Task<List<Answer>> GetAllByQuestionId(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<Answer>>($"Answer/Get-All-Answer-By-QuestionId?questionId={id}");
        }

        public async Task<Answer> GetById(int id)
        {
            var answer = await _httpClient.GetAsync($"Answer/Get-Answer-By-Id?id={id}");
            var response = await answer.Content.ReadFromJsonAsync<Answer>();
            return response;
        }

        public async Task<bool> Create(Answer answer)
        {
            var result = await _httpClient.PostAsJsonAsync<Answer>("Answer/Create-Answer", answer);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<Answer> Update(Answer answer)
        {
            var updateAnswer = await _httpClient.PutAsJsonAsync("Answer/Update-Answer", answer);
            var response = await updateAnswer.Content.ReadFromJsonAsync<Answer>();
            return response;
        }

        public async Task<Answer> UpdateStatus(int id, byte status)
        {
            var updateStatus = await _httpClient.PutAsJsonAsync($"Answer/Update-Status-Answer?questionId={id}&status={status}", status);
            var response = await updateStatus.Content.ReadFromJsonAsync<Answer>();
            return response;
        }
        public async Task<ErrorResponse> Delete(int id)
        {
            var deleteAnswer = await _httpClient.DeleteAsync($"Answer/Delete-Answer?id={id}");

            if (deleteAnswer.IsSuccessStatusCode)
            {
                return new ErrorResponse { Success = true };
            }

            var error = await deleteAnswer.Content.ReadFromJsonAsync<ErrorResponse>();
            return new ErrorResponse
            {
                Success = false,
                ErrorCode = error?.ErrorCode ?? "UNKNOWN_ERROR",
                Message = error?.Message ?? "UNKNOWN_ERROR"
            };
        }
    }
}
