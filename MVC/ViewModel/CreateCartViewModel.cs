using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModel
{
    public class CreateCartViewModel
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

    }
}
