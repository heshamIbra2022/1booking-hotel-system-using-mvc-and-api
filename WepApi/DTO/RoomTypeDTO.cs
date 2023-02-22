using System.Collections.Generic;
using WepApi.Entities;

namespace WepApi.DTO

{
    public class RoomTypeDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public RoomTypeDTO()
        {
                
        }

        public RoomTypeDTO(RoomType roomType)
        {
            Id = roomType.Id;   
            Name = roomType.Name;   
        }


        public RoomType ConvertViewModel(RoomTypeDTO roomDTO)
        {
            return new RoomType
            {
                Id = (int)roomDTO.Id,
                Name = roomDTO.Name,
            };



        }
    }

}
