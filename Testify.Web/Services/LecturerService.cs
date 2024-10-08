﻿using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class LecturerService
    {
        private readonly HttpClient _httpClient;
        public LecturerService(HttpClient httpClien)
        {
                _httpClient = httpClien;
        }

        public async Task<List<User>> GetAllLecturer()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("Lecturer/Get-All-Lecturer");
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
