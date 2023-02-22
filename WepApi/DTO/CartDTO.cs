using System;
using System.ComponentModel.DataAnnotations;
using WepApi.Entities;

namespace WepApi.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }


        public int RoomId { get; set; }
        public string CustomerId { get; set; }
        [DisplayFormat(DataFormatString = "d/M/yyyy")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [DisplayFormat(DataFormatString = "d/M/yyyy")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalDays { get; set; }
        public RoomDTO Room { get; set; }=new RoomDTO();    
        public AppUserDTO Customer { get; set; }=new AppUserDTO();
        public CartDTO()
        {
                            
        }
        public CartDTO(Cart cart )
        {
            Id = cart.Id;
            RoomId = cart.RoomId;
            FromDate = cart.FromDate;
            ToDate = cart.ToDate;
            TotalPrice = cart.TotalPrice;
            TotalDays = cart.TotalDays;
          
            CustomerId = cart.CustomerId;
            if(cart.Room != null)
            {
             Room=new RoomDTO(cart.Room);   
            }
            if(cart.Customer != null)
            {
                Customer.Id = cart.Customer.Id;
                Customer.UserName = cart.Customer.UserName;
                Customer.Address = cart.Customer.Address;
                Customer.City = cart.Customer.City; 
            }
        }


        public OrderDetails convertToOrderDetails(CartDTO cartDTO)
        {
            return new OrderDetails
            {

                RoomId = cartDTO.RoomId,
                TotallDays = cartDTO.TotalDays,
                TotalPrice = cartDTO.TotalPrice,


            };
        }
    }
}
