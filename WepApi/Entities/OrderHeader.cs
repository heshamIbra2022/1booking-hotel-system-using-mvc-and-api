using System;
using System.Collections.Generic;

namespace WepApi.Entities
{
    public class OrderHeader
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }

        public DateTime DateOfOrder { get; set; }
        public decimal OrderTotal { get; set; }

        public string OrderStatus { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public List<OrderDetails> OrderDetails { get;set;}

    }
}
