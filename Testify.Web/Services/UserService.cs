using Testify.API.DTOs;
using Testify.DAL.Models;

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

    }
}
