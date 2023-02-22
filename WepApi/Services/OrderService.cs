using System.Collections.Generic;
using System.Linq;
using WepApi.ContextModel;
using WepApi.DTO;
using WepApi.Entities;
using WepApi.GenericRepo;

namespace WepApi.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork _unitOfWork;
        ICartService _CartService;
        ApplicationDbContext _context;
        public OrderService(IUnitOfWork unitOfWork, ICartService cartService, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _CartService = cartService;
            _context = context;
        }

        //public void InsertOrder(CreateOrderHeaderDTO orderDTO,string userId)
        //{

        //    var model = new CreateOrderHeaderDTO().ConvertViewModel(orderDTO);
        //    _unitOfWork.GenericRepository<OrderHeader>().Add(model);
        //    _unitOfWork.save();
        //    var userCarts = _CartService.GetAll(userId);

        //    foreach (var userCart in userCarts)
        //    {
        //        var orderdetail = new OrderDetails
        //        {
        //            RoomId = userCart.RoomId,
        //            TotallDays = userCart.TotalDays,
        //            TotalPrice = userCart.TotalPrice,
        //            OrderHeaderId = model.Id
        //        };
        //        _unitOfWork.GenericRepository<OrderDetails>().Add(orderdetail);
        //        _unitOfWork.save();
        //    }


        //}


        public void InsertOrder(CreateOrderHeaderDTO orderDTO, string userId)
        {

            var model = new CreateOrderHeaderDTO().ConvertViewModel(orderDTO);
            //_unitOfWork.GenericRepository<OrderHeader>().Add(model);
            //_unitOfWork.save();
            _context.OrderHeaders.Add(model);
            _context.SaveChanges();

            var userCarts = _context.Carts.Where(x => x.CustomerId == userId).ToList();

         

            foreach (var userCart in userCarts)
            {
                var orderdetail = new OrderDetails
                {
                    RoomId = userCart.RoomId,
                    TotallDays = userCart.TotalDays,
                    TotalPrice = userCart.TotalPrice,
                    OrderHeaderId = model.Id
                };
                _context.OrderDetails.Add(orderdetail);
                _context.SaveChanges();
        
            }

            _context.Carts.RemoveRange(userCarts);
            _context.SaveChanges();

        }


        public List<CreateOrderHeaderDTO> GetOrdersList(string? userId)
        {
           List<CreateOrderHeaderDTO> orders =null;
            if(userId==null)
            {
              orders=  _context.OrderHeaders.Select(x => new CreateOrderHeaderDTO(x)).ToList();
            }
            else
            {
              orders = _context.OrderHeaders.Where(x => x.CustomerId == userId).Select(x => new CreateOrderHeaderDTO(x)).ToList();
            }

            return orders;
        }

        public void DeleteOrder(int id)
        {
            var model = _unitOfWork.GenericRepository<OrderHeader>().GetById(id);
            _unitOfWork.GenericRepository<OrderHeader>().Delete(model);
            _unitOfWork.save();
        }

    }
}
