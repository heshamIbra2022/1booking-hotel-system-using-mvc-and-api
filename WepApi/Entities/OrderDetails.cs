namespace WepApi.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; } 
        
        public int OrderHeaderId { get; set; }  
        public OrderHeader OrderHeader { get; set; }  
        
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public decimal TotalPrice { get; set; }
        public int TotallDays { get; set; }
    }
}
