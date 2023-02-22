using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepApi.DTO;
using WepApi.Services;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        [HttpGet("getAllRomTypes")]
        public IActionResult Index()
        {
           
         var roomTypes=   _roomTypeService.GetAll();
            if (roomTypes == null) { 
                return NotFound();
            }
            return Ok(roomTypes);

        }

      
        [HttpPost("addnewRoomType")]
        public IActionResult Create(RoomTypeDTO roomTypeDTO)
        {
            if (ModelState.IsValid ==false)
            {
                return BadRequest(ModelState);
            }
            _roomTypeService.InsertRoomType(roomTypeDTO);
            return Ok(roomTypeDTO);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                var roomType = _roomTypeService.GetRoomTypeById(id);
                return Ok(roomType);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut]
        public IActionResult Edit(RoomTypeDTO roomTypeDTO)
        {
            if (ModelState.IsValid == true)
            {
               
                _roomTypeService.UpdateRoomType(roomTypeDTO);
                return Ok(roomTypeDTO);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
_roomTypeService.DeleteRoomType(id);
                return Ok("deleted");
            }catch
            {
            return BadRequest();  
            }
        }
    }
}
