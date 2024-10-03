using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class ExamService
    {
        HttpClient _httpClient;
        public ExamService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<List<Exam>> GetAllActive()
        {
            var listEx = await _httpClient.GetFromJsonAsync<List<Exam>>("Exam/Get-Active");
            return listEx;
        }

    }
}
