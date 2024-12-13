using Testify.API.DTOs;
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

        public async Task<List<ClassWithUser>> GetClassInSchedule(int scheduleId)
        {
            return await _httpClient.GetFromJsonAsync<List<ClassWithUser>>("ClassExamSchedule/Get-Class-ByScheduleId?scheduleId=" + scheduleId);
        }

        public async Task<bool> AddListClassToSchedule(List<ClassWithUser> data, int scheduleId)
        {
            var lis = await CheckListClassInSchedule(data, scheduleId);
            var a = await _httpClient.PostAsJsonAsync("ClassExamSchedule/Add-ListClassToSchedule?scheduleId=" + scheduleId, data);
            if (a.IsSuccessStatusCode)
                return true;
            var error = await a.Content.ReadAsStringAsync();
            return false;
        }

        public async Task<bool> RemoveListClassToSchedule(List<ClassWithUser> data, int scheduleId)
        {
            var a = await _httpClient.PostAsJsonAsync("ClassExamSchedule/Remove-ListClassToSchedule?scheduleId=" + scheduleId, data);
            if (a.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<User>> CheckListClassInSchedule(List<ClassWithUser> data, int scheduleId)
        {
            CheckClassScheduleRequest reqData = new CheckClassScheduleRequest();
            reqData.DataList = data;
            reqData.ScheduleId = scheduleId;
            var a = await _httpClient.PostAsJsonAsync<CheckClassScheduleRequest>("ClassExamSchedule/Checks-StudentExist-InSchedule", reqData);

            if (a.IsSuccessStatusCode)
            {
                if (a.Content != null)
                {
                    try
                    {
                        var response = await a.Content.ReadFromJsonAsync<List<User>>();

                        return response;
                    }
                    catch (Exception e)
                    {
                    }



                }
            }
            return null;

        }

    }

}
