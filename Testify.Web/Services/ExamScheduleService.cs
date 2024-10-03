using Testify.API.DTOs;
using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class ExamScheduleService
    {
        private readonly HttpClient _httpClient;
        public ExamScheduleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ExamSchedule>> GetAll()
        {
            var lst = await _httpClient.GetFromJsonAsync<List<ExamSchedule>>("ExamSchedule/Get-All");
            return lst;
        }

        public async Task<List<ExamSchedule>> GetFuture()
        {
            var lst = await _httpClient.GetFromJsonAsync<List<ExamSchedule>>("ExamSchedule/Get-Future");
            return lst;
        }

        public async Task<List<ExamSchedule>> GetPast()
        {
            var lst = await _httpClient.GetFromJsonAsync<List<ExamSchedule>>("ExamSchedule/Get-Past");
            return lst;
        }

        public async Task<List<ExamSchedule>> GetActive()
        {
            var lst = await _httpClient.GetFromJsonAsync<List<ExamSchedule>>("ExamSchedule/Get-Active");
            return lst;
        }

        public async Task<List<ExamSchedule>> GetCurrent()
        {
            var lst = await _httpClient.GetFromJsonAsync<List<ExamSchedule>>("ExamSchedule/Get-Current");
            return lst;
        }


        public async Task<bool> Update(ExamSchedule schedule)
        {
            var lst = await _httpClient.PutAsJsonAsync("ExamSchedule/Get-Current", schedule);

            if (lst.IsSuccessStatusCode)
            {
                return true;
            }
            return false;


        }

        public async Task<bool> Delete(int id)
        {
            var lst = await _httpClient.DeleteAsync($"ExamSchedule/Get-Current?id={id}");

            if (lst.IsSuccessStatusCode)
            {
                return true;
            }
            return false;


        }

        public async Task<bool> Create(ExamSchedule schedule)
        {
            var stats = await _httpClient.PostAsJsonAsync("ExamSchedule/Create", schedule);

            if (stats.IsSuccessStatusCode)
            {
                return true;

            }
            else { return false; }


        }

        public async Task<List<ExamScheduleDto>> GetInforBasic()
        {
            var lst = await _httpClient.GetFromJsonAsync<List<ExamScheduleDto>>("ExamSchedule/Get-InfoBasic");
            return lst;
        }

        public async Task<ExamSchedule> GetInTimeRange(DateTime start, DateTime end)
        {
            var schedule = await _httpClient.GetFromJsonAsync<ExamSchedule>("ExamSchedule/Get-InTime");
            return schedule;
        }

    }
}
