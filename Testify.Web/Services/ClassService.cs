﻿using Newtonsoft.Json;
using System.Collections.Generic;
using Testify.DAL.Models;

namespace Testify.Web.Services
{
    public class ClassService
    {
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
    }
}
