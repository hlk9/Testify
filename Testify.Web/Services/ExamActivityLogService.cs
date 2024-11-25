using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class ExamActivityLogService
    {
        HttpClient httpClient;

        public ExamActivityLogService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<List<ExamActivityLog>> GetAllByUserId(Guid id)
        {
            return await httpClient.GetFromJsonAsync<List<ExamActivityLog>>("ExamActivityLog/GetAllByUserId?id=" + id);
        }

        public async Task<List<ExamActivityLog>> GetAllByExamId(int id)
        {
            return await httpClient.GetFromJsonAsync<List<ExamActivityLog>>("ExamActivityLog/GetAllByExamId?id=" + id);
        }

        public async Task<List<ExamActivityLog>> GetAllByUserAndExamId(Guid uId, int eId)
        {
            return await httpClient.GetFromJsonAsync<List<ExamActivityLog>>("ExamActivityLog/GetAllByUidAndEid?Uid=" + uId + "&Eid=" + eId);
        }

        public async Task<bool> Create(ExamActivityLog log)
        {

            var stat = await httpClient.PostAsJsonAsync("ExamActivityLog/AddExamLog", log);

            if (stat.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }

      

    }
}
