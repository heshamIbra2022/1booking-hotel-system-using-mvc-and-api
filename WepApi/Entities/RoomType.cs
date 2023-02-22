using System.Collections.Generic;

namespace WepApi.Entities
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
