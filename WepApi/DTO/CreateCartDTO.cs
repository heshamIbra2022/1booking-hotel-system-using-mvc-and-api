using System;
using WepApi.Entities;

namespace WepApi.DTO
{
    public class CreateCartDTO
    {
        public int Id { get; set; }


        public int RoomId { get; set; }
      
        public string CustomerId { get; set; }
      
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalDays { get; set; }

        public CreateCartDTO()
        {

        }
        public CreateCartDTO(Cart cart)
        {
            Id = cart.Id;
            RoomId = cart.RoomId;
            CustomerId = cart.CustomerId;
            FromDate = cart.FromDate;
            ToDate = cart.ToDate;
            TotalPrice = cart.TotalPrice;
            TotalDays = cart.TotalDays;


        }
        public Cart ConvertViewModel(CreateCartDTO createCartDTO)
        {
            return new Cart
            {
                Id = createCartDTO.Id,
                RoomId = createCartDTO.RoomId,
                CustomerId = createCartDTO.CustomerId,
                FromDate = createCartDTO.FromDate,
                ToDate = createCartDTO.ToDate,
                TotalPrice = createCartDTO.TotalPrice,
                TotalDays = createCartDTO.TotalDays,

            };
        }

    }
}
