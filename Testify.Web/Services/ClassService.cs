using Newtonsoft.Json;
using System.Collections.Generic;
using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class ClassService
    {
        private readonly string baseApiUrl;
        public ClassService(IConfiguration configuration)
        {
            baseApiUrl = configuration["AppSettings:APIBaseURL"];
        }

        public async Task <List<Class>>GetAllClass()
        {
            using (var client = new HttpClient()) { 
            client.BaseAddress = new Uri(baseApiUrl);
                var response = client.GetStringAsync("Class/Get-Classes?organizationId=1").Result;
                var lst = JsonConvert.DeserializeObject<List<Class>>(response);
                return lst;
            }
        }
    }
}
