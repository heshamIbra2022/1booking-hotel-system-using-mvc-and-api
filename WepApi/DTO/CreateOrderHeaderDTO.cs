using System;
using WepApi.Entities;

namespace WepApi.DTO
{
    public class CreateOrderHeaderDTO
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }
  

        public DateTime DateOfOrder { get; set; }
        public decimal OrderTotal { get; set; }

        public string OrderStatus { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }


        public CreateOrderHeaderDTO()
        {

        }

        public CreateOrderHeaderDTO(OrderHeader orderHeader)
        {
            Id = orderHeader.Id;
            CustomerId = orderHeader.CustomerId;
            DateOfOrder = orderHeader.DateOfOrder;
            OrderTotal = orderHeader.OrderTotal;
            OrderStatus = orderHeader.OrderStatus;
            Name = orderHeader.Name;
            City = orderHeader.City;
            Address = orderHeader.Address;


        }
        public OrderHeader ConvertViewModel(CreateOrderHeaderDTO orderDTO)
        {
            return new OrderHeader
            {
                Id = orderDTO.Id,
                CustomerId = orderDTO.CustomerId,
                DateOfOrder = orderDTO.DateOfOrder,
                OrderTotal = orderDTO.OrderTotal,
                OrderStatus = orderDTO.OrderStatus,
                Name = orderDTO.Name,
                City = orderDTO.City,
                Address = orderDTO.Address,

            };
        }
    }
}
