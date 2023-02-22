using System.Collections.Generic;
using WepApi.DTO;

namespace WepApi.Services
{
    public interface IRoomService
    {
        List<RoomDTO> GetAll(int?BranchId);
      
       RoomDTO GetRoomById(int id);
       // CreateRoomDTO GetRoomById(int id);
        void UpdateRoom(RoomDTO roomDTO);
        void DeleteRoom(int id);
        void InsertRoom(RoomDTO roomDTO);
        public BranchesWithRoomTypesViewModel GetBranchesWRoomTypes();
    }
}
