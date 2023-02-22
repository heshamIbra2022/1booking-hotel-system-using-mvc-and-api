using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WepApi.Entities;

namespace WepApi.DTO
{
    public class BranchesWithRoomTypesViewModel
    {
        public List<BranchDTO> ListBranches { get; set; }
        public List<RoomTypeDTO> ListRoomTypes { get; set; } 
    }
}
