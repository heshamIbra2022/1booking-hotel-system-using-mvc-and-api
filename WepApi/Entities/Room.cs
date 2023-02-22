namespace WepApi.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }   
       
        public decimal price { get; set; }
        public byte [] PictureImg { get; set; }
        public RoomAvaible Status { get; set; }

        public int RoomBranchId { get; set; }
        public Branch RoomBranch { get; set; }

        public int RoomTypeId { get; set; } 
        public RoomType RoomType { get; set; }  

    }

    public enum RoomAvaible
    {
        Booked=0, 
        Available

    }
}
