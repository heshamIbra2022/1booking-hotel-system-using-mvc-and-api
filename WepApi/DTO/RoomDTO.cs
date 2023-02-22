using WepApi.Entities;

namespace WepApi.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }

        public decimal price { get; set; }
        public byte[] PictureImg { get; set; }
        public RoomAvaible Status { get; set; }

        public int RoomBranchId { get; set; }

        

        public int RoomTypeId { get; set; }
       
        public RoomTypeDTO RoomType { get; set; }=new RoomTypeDTO();
        public BranchDTO Branch{ get; set; }=new BranchDTO();
        public RoomDTO()
        {

        }

        public RoomDTO(Room room)
        {
            Id = room.Id;
            RoomNumber = room.RoomNumber;
            price = room.price;
            PictureImg = room.PictureImg;
            Status = room.Status;
            RoomBranchId = room.RoomBranchId;
            RoomTypeId = room.RoomTypeId;
            if(room.RoomType != null)
            {
                RoomType.Id = room.RoomType.Id;
                RoomType.Name = room.RoomType.Name;
            }
           if(room.RoomBranch != null) {
                Branch.Id = room.RoomBranch.Id;
                Branch.Name = room.RoomBranch.Name;

            }
           


        }

        public Room ConvertViewModel(RoomDTO roomDTO)
        {
            return new Room
            {
                Id = roomDTO.Id,
                RoomNumber = roomDTO.RoomNumber,
                price = roomDTO.price,
                PictureImg = roomDTO.PictureImg,
                Status = roomDTO.Status,
                RoomBranchId = roomDTO.RoomBranchId,

                RoomTypeId = roomDTO.RoomTypeId,


            };


        }
    }
}
