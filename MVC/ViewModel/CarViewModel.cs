using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModel
{ 
    public class CartViewModel
    {
        public int Id { get; set; }


        public int RoomId { get; set; }
        public string CustomerId { get; set; }
      
        public DateTime FromDate { get; set; }
       

        public DateTime ToDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalDays { get; set; }
        public RoomViewModel Room { get; set; }
        public AppUserViewModel Customer { get; set; }
     
      
    }
}
