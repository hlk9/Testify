using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.Web.Services
{
    public class ClassExamScheduleService
    {
        private readonly HttpClient _httpClient;

        public ClassExamScheduleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public  async Task<List<ClassWithUser>> GetClassInSchedule(int scheduleId)
        {
            return await _httpClient.GetFromJsonAsync<List<ClassWithUser>>("ClassExamSchedule/Get-Class-ByScheduleId?scheduleId=" +scheduleId);
        }
    }
    
}
