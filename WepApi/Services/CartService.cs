using System.Collections.Generic;
using System.Linq;
using WepApi.DTO;
using WepApi.Entities;
using WepApi.GenericRepo;

namespace WepApi.Services
{
    public class CartService : ICartService
    {
        IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public void DeleteCart(int id)
        //{
        //    throw new System.NotImplementedException();
        //}

        public List<CartDTO> GetAll(string userId)
        {
           
                var modelList = _unitOfWork.GenericRepository<Cart>().GetAll(c => c.CustomerId == userId, includeProperties: "Room,Customer,Room.RoomType,Room.RoomBranch").Select(x =>new CartDTO(x) ).ToList();
           
           
           
            return modelList;
        }
       public void DeleteCart(int id)
        {
            var model = _unitOfWork.GenericRepository<Cart>().GetById(id);
            _unitOfWork.GenericRepository<Cart>().Delete(model);
            _unitOfWork.save();
        }
        public Cart GetCartByRoomId(int roomId, string userId)
        {
            return _unitOfWork.GenericRepository<Cart>().GetAll(x => x.CustomerId == userId).FirstOrDefault(x => x.RoomId == roomId);
        }

        public void InsertCart(CreateCartDTO cartDTO)
        {
            var model = new CreateCartDTO().ConvertViewModel(cartDTO);
            model.TotalDays = (int)(model.ToDate - model.FromDate).TotalDays;
          var price=  _unitOfWork.GenericRepository<Room>().GetById(model.RoomId).price;
            model.TotalPrice=price*model.TotalDays;
            _unitOfWork.GenericRepository<Cart>().Add(model);
            _unitOfWork.save();
        }

        public CreateCartDTO GetCartById(int id)
        {
            var cart = _unitOfWork.GenericRepository<Cart>().GetById(id);
            var cartDto=new CreateCartDTO(cart);
            return cartDto; 
        }

        public void UpdateCart(CreateCartDTO cartDTO)
        {
            var cartInDb = _unitOfWork.GenericRepository<Cart>().GetById(cartDTO.Id);
            
           
            cartInDb.FromDate = cartDTO.FromDate;
            cartInDb.ToDate = cartDTO.ToDate;
            cartInDb.TotalDays= (int)(cartDTO.ToDate - cartDTO.FromDate).TotalDays;
            var price = _unitOfWork.GenericRepository<Room>().GetById(cartDTO.RoomId).price;
           cartInDb.TotalPrice = price * cartInDb.TotalDays;
            _unitOfWork.GenericRepository<Cart>().Update(cartInDb);
            _unitOfWork.save();

        }

        //public void UpdateCart(CartDTO cartDTO)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
