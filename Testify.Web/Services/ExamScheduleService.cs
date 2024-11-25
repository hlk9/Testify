using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using Testify.API.DTOs;
using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class ExamScheduleService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;
        public ExamScheduleService(HttpClient httpClient, TokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;

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
            var lst = await _httpClient.PutAsJsonAsync("ExamSchedule/Update", schedule);

            if (lst.IsSuccessStatusCode)
            {
                return true;
            }
            return false;


        }

        public async Task<bool> Delete(int id)
        {
            var lst = await _httpClient.DeleteAsync($"ExamSchedule/Delete?id={id}");

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
            var token = await _tokenService.GetTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var lst = await _httpClient.GetFromJsonAsync<List<ExamScheduleDto>>("ExamSchedule/Get-InfoBasic");
                return lst;
            }
            return null;

        }

        public async Task<ExamSchedule> GetInTimeRange(DateTime start, DateTime end, int? subjectId)
        {
            var startStr = Uri.EscapeDataString(start.ToString("MM/dd/yyyy HH:mm:ss"));
            var endStr = Uri.EscapeDataString(end.ToString("MM/dd/yyyy HH:mm:ss"));

            var response = await _httpClient.GetAsync($"ExamSchedule/Get-InTime?start={startStr}&end={endStr}&subjectId={subjectId}");

            if (response.StatusCode == HttpStatusCode.NoContent) // 204 No Content
            {

                return null;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();


                var schedule = JsonConvert.DeserializeObject<ExamSchedule>(content);
                return schedule;

            }



        }

        public async Task<ExamSchedule> GetById(int? id)
        {
            var schedule = await _httpClient.GetFromJsonAsync<ExamSchedule>("ExamSchedule/Get-ById?id=" + id.ToString());
            return schedule;
        }

        public async Task<List<ExamSchedule>> GetExamScheduleTimesByClassUserIdAsync(Guid userId)
        {
            var obj = await _httpClient.GetFromJsonAsync<List<ExamSchedule>>($"ExamSchedule/Get-ExamScheduleTimes-By-ClassUserIdAsync?userId={userId}");
            return obj;
        }

        public async Task<List<ExamScheduleDto>> GetAllScheduleByStudentId(string studentId)
        {
            var obj = await _httpClient.GetFromJsonAsync<List<ExamScheduleDto>>($"ExamSchedule/Get-All-Schedule-ByStudentId?studentId={studentId}");
            return obj;
        }

        
            public async Task<List<ExamSchedule>> GetAllExamScheduleByUserId(Guid userId)
        {
            var count = await _httpClient.GetAsync($"ExamSchedule/Get-ExamSchedule-By-UserId?userId={userId}");
            var response = await count.Content.ReadFromJsonAsync<List<ExamSchedule>>();
            return response;
        }
    }
}
