using System;

namespace MVC.ViewModel
{
    public class CreateOrderHeaderViewModel
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }


        public DateTime DateOfOrder { get; set; }
        public decimal OrderTotal { get; set; }

        public string OrderStatus { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

    }
}
