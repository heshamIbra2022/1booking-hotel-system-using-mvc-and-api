using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepApi.DTO;
using WepApi.Services;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("getRooms/{BranchId?}")]
        public IActionResult Index(int? BranchId=null)
        {
            var rooms = _roomService.GetAll(BranchId);
            if (rooms == null)
            {
                return NotFound();
            }
            return Ok(rooms);
        }

        [HttpGet("getBranchesWithRoomTypes")]
        public IActionResult Index2()
        {
            var data = _roomService.GetBranchesWRoomTypes();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpPost("addnewRoom")]
        public IActionResult Create(RoomDTO createRoomDTO)
        {
            try
            {
                _roomService.InsertRoom(createRoomDTO);
                return Ok(createRoomDTO);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                var room = _roomService.GetRoomById(id);
                return Ok(room);
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpPut]
        public IActionResult Edit(RoomDTO RoomDTO)
        {
            if (ModelState.IsValid == true)
            {
                _roomService.UpdateRoom(RoomDTO);
                return Ok(RoomDTO);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteRoom(int id)
        {
            try
            {
                _roomService.DeleteRoom(id);
                return Ok("deleted");
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}

