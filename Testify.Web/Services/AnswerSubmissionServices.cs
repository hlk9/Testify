using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class AnswerSubmissionServices
    {
        HttpClient _httpClient;

        public AnswerSubmissionServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AnswerSubmission> CreateAnswerSubmission(AnswerSubmission answerSubmission)
        {
            var obj = await _httpClient.PostAsJsonAsync("AnswerSubmission/Create-AnswerSubmission", answerSubmission);
            var reponse = await obj.Content.ReadFromJsonAsync<AnswerSubmission>();

            return reponse;
        }

        public async Task<List<AnswerSubmission>> GetBySubmissionId(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<AnswerSubmission>>($"AnswerSubmission/Get-AnswerById?id={id}");
        }
    }
}
