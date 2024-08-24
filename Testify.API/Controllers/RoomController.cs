using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("Room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        RoomRepository roomRepository;
        public RoomController()
        {
            roomRepository = new RoomRepository();
        }

        [HttpGet("get-all-room")]
        public async Task<ActionResult<List<Room>>> GetAll()
        {
            var lstRoom = await roomRepository.GetAllRoom();
            return Ok(lstRoom);
        }

        [HttpGet("get-room-by-id")]
        public async Task<ActionResult<Room>> GetByIdRoom(int id)
        {
            var objRoom = await roomRepository.GetRoomById(id);
            return Ok(objRoom);
        }

        [HttpPost("create-room")]
        public async Task<ActionResult<Room>> Create(Room r)
        {
            var addRoom = await roomRepository.CreateRoom(r);
            return Ok(addRoom);
        }

        [HttpPut("update-room")]
        public async Task<ActionResult<Room>> Update(Room r)
        {
            var updateRoom = await roomRepository.UpdateRoom(r);
            return Ok(updateRoom);
        }

        [HttpDelete("delete-room")]
        public async Task<ActionResult<Room>> Delete(int id)
        {
            var deleteRoom = await roomRepository.DeleteRoom(id);
            return Ok(deleteRoom);
        }
    }
}
