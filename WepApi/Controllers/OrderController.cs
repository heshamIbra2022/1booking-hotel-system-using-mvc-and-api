using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using WepApi.DTO;
using WepApi.Services;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            try {
            List<CreateOrderHeaderDTO> orders = null;
            if (User.IsInRole("Admin"))
            {
               orders=  _orderService.GetOrdersList(null);
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                 orders = _orderService.GetOrdersList(userId);
            }
            return Ok(orders);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(CreateOrderHeaderDTO orderHeaderDTO)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                _orderService.InsertOrder(orderHeaderDTO,userId);
                return Ok(orderHeaderDTO);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
                return Ok("deleted");
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
