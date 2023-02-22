using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WepApi.DTO;
using WepApi.Services;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
       private ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try {
         var claimsIdentity=(ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
          var carts=  _cartService.GetAll(userId);
            return Ok(carts);
            }
            catch
            {
                return BadRequest();
            }
        }



        [HttpPost]
        public IActionResult Create(CreateCartDTO createCartDTO)
        {
            try
            {
              var cart=  _cartService.GetCartByRoomId(createCartDTO.RoomId,createCartDTO.CustomerId);
                if (cart == null) {
                    _cartService.InsertCart(createCartDTO);
                }
              
                return Ok(createCartDTO);
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteCart(int id)
        {
            try
            {
               _cartService.DeleteCart(id);
                return Ok("deleted");
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
                var cart = _cartService.GetCartById(id);
                return Ok(cart);
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpPut]
        public IActionResult Edit(CreateCartDTO cartDTO)
        {
            if (ModelState.IsValid == true)
            {
                _cartService.UpdateCart(cartDTO);
                return Ok(cartDTO);
            }
            return BadRequest();
        }
    }
}
