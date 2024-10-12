using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.Web.Services
{
    public class LecturerService
    {
        private readonly HttpClient _httpClient;
        public LecturerService(HttpClient httpClien)
        {
                _httpClient = httpClien;
        }

        public async Task<List<User>> GetAllLecturer(string? textSearch, bool isActive)
        {
            var allUsers = await _httpClient.GetAsync($"Lecturer/Get-All-Lecturer?keyWord={textSearch}&isActive={isActive}");
            var response = await allUsers.Content.ReadFromJsonAsync<List<User>>();

            return response;
        }

        public async Task<List<ScoreStatistics>> GetScore(int idSub, int idExam)
        {
            var allScore = await _httpClient.GetAsync($"Lecturer/Get-score?idsub={idSub}?idexam={idExam}");
            var response = await allScore.Content.ReadFromJsonAsync<List<ScoreStatistics>>();

            return response;
        }

        public async Task<User> GetLecturerById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"Lecturer/Get-Lecturer-By-Id?id={id}");
        }

        public async Task<List<User>> GetAllTeacher()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("Lecturer/Get-All-Teacher");
        }

        public async Task<bool> CreateLecturer(User user)
        {
            var statusCreate = await _httpClient.PostAsJsonAsync<User>("Lecturer/Create-Lecturer", user);
            if (statusCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateLecturer(User user)
        {
            var statusUpdate = await _httpClient.PutAsJsonAsync<User>("Lecturer/Update-Lecturer", user);
            if (statusUpdate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteLecturer(Guid id)
        {
            var statusDelete = await _httpClient.DeleteAsync($"Lecturer/Delete-Lecturer?id={id}");
            if (statusDelete.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
