using System.Collections.Generic;
using WepApi.DTO;
using WepApi.Entities;

namespace WepApi.Services
{
    public interface ICartService
    {
        List<CartDTO> GetAll(string userId);
       CreateCartDTO GetCartById(int id);
        void UpdateCart(CreateCartDTO cartDTO);
       void DeleteCart(int id);
      public  Cart GetCartByRoomId (int roomId,string userId);
       void InsertCart(CreateCartDTO cartDTO);
    }
}
