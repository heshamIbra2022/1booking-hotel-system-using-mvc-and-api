using Microsoft.AspNetCore.Http;

namespace MVC.ViewModel
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }

        public decimal price { get; set; }
        public byte [] PictureImg { get; set; }
        public RoomAvaible Status { get; set; }

        public int RoomBranchId { get; set; }



        public int RoomTypeId { get; set; }

        public BranchViewModel Branch { get; set; }
        public RoomTypeViewModel RoomType { get; set; }


      
    }

    public enum RoomAvaible
    {
        Booked = 0,
        Available

    }
}
