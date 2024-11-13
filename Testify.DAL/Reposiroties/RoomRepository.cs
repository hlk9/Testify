using Microsoft.EntityFrameworkCore;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class RoomRepository
    {
        TestifyDbContext _context;
        public RoomRepository()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<Room>> GetAllRoom()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomById(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<Room> CreateRoom(Room r)
        {
            try
            {
                var addRoom = _context.Rooms.Add(r).Entity;
                await _context.SaveChangesAsync();
                return addRoom;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Room> UpdateRoom(Room r)
        {
            try
            {
                var updateRoom = _context.Rooms.Find(r.Id);

                updateRoom.Name = r.Name;
                updateRoom.Status = r.Status;
                updateRoom.Capacity = r.Capacity;
                var objRoom = _context.Rooms.Update(updateRoom).Entity;
                await _context.SaveChangesAsync();
                return objRoom;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Room> DeleteRoom(int id)
        {
            try
            {
                var deleteRoom = _context.Rooms.Find(id);

                var objRoom = _context.Rooms.Remove(deleteRoom).Entity;
                await _context.SaveChangesAsync();
                return objRoom;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
