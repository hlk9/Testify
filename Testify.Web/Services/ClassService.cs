using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.Web.Services
{
    public class ClassService
    {
        private readonly HttpClient _httpClient;
        public ClassService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClassWithUser>> GetAllClass(string? textSearch, bool isActive)
        {
            var allClass = await _httpClient.GetAsync($"Class/Get-Classes?KeyWord={textSearch}&isActive={isActive}");
            var response = await allClass.Content.ReadFromJsonAsync<List<ClassWithUser>>();
            return response;
        }

        public async Task<List<Class>> GetClassList(string? textSearch, bool isActive)
        {
            var allClass = await _httpClient.GetAsync($"Class/Get-ClassList?KeyWord={textSearch}&isActive={isActive}");
            var response = await allClass.Content.ReadFromJsonAsync<List<Class>>();
            return response;
        }

        public async Task<Class> GetClassId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Class>($"Class/get-classes-by-id?id={id}");
        }

        public async Task<Class> GetClassByCode(string classCode)
        {
            return await _httpClient.GetFromJsonAsync<Class>($"Class/Get-Class-By-ClassCode?ClassCode={classCode}");
        }

        public async Task<Class> CreateClass(Class c)
        {
            var newClass = await _httpClient.PostAsJsonAsync("Class/Add-Class", c);
            var reponse = await newClass.Content.ReadFromJsonAsync<Class>();
            return reponse;

        }

        public async Task<Class> UpdateClass(Class c)
        {
            var updateClass = await _httpClient.PutAsJsonAsync("Class/Update-Class", c);
            var reponse = await updateClass.Content.ReadFromJsonAsync<Class>();
            return reponse;
        }

        public async Task<Class> UpdateStatus(int id, byte status)
        {
            var updateStatus = await _httpClient.PutAsJsonAsync($"Class/Update-Status?classId={id}&status={status}", status);
            var reponse = await updateStatus.Content.ReadFromJsonAsync<Class>();
            return reponse;
        }

        public async Task<bool> DeleteClass(int id)
        {
            var status = await _httpClient.DeleteAsync($"Class/Delete-Class?id={id}");
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ClassWithUser>> GetClassBySubjectId(int? subjectId,int scheduleId)
        {
            var listClass = await _httpClient.GetFromJsonAsync<List<ClassWithUser>>($"Class/Get-Classes-BySubjectIdExcludeInSchedule?subjectId=" + subjectId+ "&scheduleId="+scheduleId);
            return listClass;
        }

        public async Task<List<Class>>
            GetClassByTeacherId(Guid? Id) => await _httpClient.GetFromJsonAsync<List<Class>>($"Class/get-classes-by-TeacherID?id=" + Id);
    }
}
