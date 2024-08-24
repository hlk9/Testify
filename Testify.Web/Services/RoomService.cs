using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.Web.Services
{
    public class RoomService
    {
        private readonly HttpClient _httpClient;
        public RoomService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Room>> GetAllRoom()
        {
            return await _httpClient.GetFromJsonAsync<List<Room>>("Room/get-all-room");
        }

        public async Task<Room> GetRoomId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Room>($"Room/get-room-by-id?id={id}");
        }

        public async Task<bool> CreateRoom(Room r)
        {
            var status = await _httpClient.PostAsJsonAsync<Room>("Room/create-room", r);
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateRoom(Room r)
        {
            var status = await _httpClient.PutAsJsonAsync<Room>("Room/update-room", r);
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRoom(int id)
        {
            var status = await _httpClient.DeleteAsync($"Room/delete-room?id={id}");
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
