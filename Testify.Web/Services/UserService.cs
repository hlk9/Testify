using Microsoft.AspNetCore.Components.Forms;
using Testify.API.DTOs;
using Testify.DAL.Models;
using Testify.Web.Components.Pages.Examiner;

namespace Testify.Web.Services
{
    public class UserService
    {
           

        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterStudent(User user)
        {
            var uLT = await _httpClient.PostAsJsonAsync<User>("User/Register-Student", user);



            if (uLT.IsSuccessStatusCode ==  true)
            {
                return true;
            }
            return false;


        }

        public async Task<List<User>> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("User/get-all-users");
        }


        public async Task<int> ImportExcelUser(IBrowserFile file, int levelId)
        {
            using var content = new MultipartFormDataContent();

            using var stream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(stream);
            stream.Position = 0;

            content.Add(new StreamContent(stream), "file", file.Name);
            content.Add(new StringContent(levelId.ToString()), "levelId");


            var allUser = await _httpClient.PostAsync("User/Import-Excel-User", content);
            var response = await allUser.Content.ReadFromJsonAsync<int>();

            return response;


        }

        public async Task<List<User>> GetUsersWithStatus(int classId, string? searchValue)
        {
            return await _httpClient.GetFromJsonAsync<List<User>>($"User/Get-Users-With-Status-One?classId={classId}&searchValue={searchValue}");
        }

        public async Task<List<User>> GetUsersWithStatusTwo(int classId)
        {
            return await _httpClient.GetFromJsonAsync<List<User>>($"User/Get-Users-With-Status-Two?classId={classId}");
        }
    }
}
