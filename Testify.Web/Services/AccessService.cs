using System.Security.Cryptography;
using System.Text;
using Testify.API.DTOs;

namespace Testify.Web.Services
{
    public class AccessService
    {
        private readonly HttpClient _httpClient;

        public AccessService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string HashPassword(string password)
        {
            //4297F44B13955235245B2497399D7A93 
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            md5.Clear();
            return sb.ToString();

        }

        public async Task<UserLoginWithToken> Login(string username, string password)
        {         
            var uLT = await _httpClient.GetFromJsonAsync<UserLoginWithToken>($"/Access/Login?username={username}&passwordHash={HashPassword(password)}");
           
            if (uLT != null)
            {
                return uLT;
            }
            return null;


        }
    }
}
