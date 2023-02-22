using System.Collections.Generic;
using WepApi.DTO;

namespace WepApi.Services
{
    public interface IOrderService
    {
        public void InsertOrder(CreateOrderHeaderDTO orderDTO, string userId);
        public List<CreateOrderHeaderDTO> GetOrdersList(string? userId);
        public void DeleteOrder(int id);
    }
}
