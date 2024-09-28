using Newtonsoft.Json;
using System.Collections.Generic;
using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class ClassService
    {
        //private readonly string baseApiUrl;
        //public ClassService(IConfiguration configuration)
        //{
        //    baseApiUrl = configuration["AppSettings:APIBaseURL"];
        //}

        //public async Task <List<Class>>GetAllClass()
        //{
        //    using (var client = new HttpClient()) { 
        //    client.BaseAddress = new Uri(baseApiUrl);
        //        var response = client.GetStringAsync("Class/Get-Classes").Result;
        //        var lst = JsonConvert.DeserializeObject<List<Class>>(response);
        //        return lst;
        //    }
        //}
        private readonly HttpClient _httpClient;
        public ClassService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Class>> GetAllClass()
        {
            return await _httpClient.GetFromJsonAsync<List<Class>>("Class/Get-Classes");
        }

        public async Task<Class> GetClassId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Class>($"Class/get-classes-by-id?id={id}");
        }

        public async Task<bool> CreateClass(Class c)
        {
            var status = await _httpClient.PostAsJsonAsync<Class>("Room/Add-Class", c);
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateClass(Class c)
        {
            var status = await _httpClient.PutAsJsonAsync<Class>("Class/Update-Class", c);
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
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
    }
}
