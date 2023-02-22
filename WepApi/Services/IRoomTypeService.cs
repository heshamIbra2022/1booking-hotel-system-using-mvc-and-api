using System.Collections.Generic;
using WepApi.DTO;

namespace WepApi.Services
{
    public interface IRoomTypeService
    {
        List<RoomTypeDTO> GetAll();
        RoomTypeDTO GetRoomTypeById(int id);
        void UpdateRoomType(RoomTypeDTO roomTypeDTO);
        void DeleteRoomType(int id);
        void InsertRoomType(RoomTypeDTO roomTypeDTO);
    }
}
