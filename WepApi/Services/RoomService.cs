using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using WepApi.ContextModel;
using WepApi.DTO;
using WepApi.Entities;
using WepApi.GenericRepo;

namespace WepApi.Services
{
    public class RoomService : IRoomService
    {
        private IUnitOfWork _unitOfWork;
        private ApplicationDbContext _applicationDbContext;
        public RoomService(IUnitOfWork unitOfWork, ApplicationDbContext applicationDbContext)
        {
            _unitOfWork = unitOfWork;
            _applicationDbContext = applicationDbContext;
        }

        public void DeleteRoom(int id)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(id);
            _unitOfWork.GenericRepository<Room>().Delete(model);
            _unitOfWork.save();
        }

        public List<RoomDTO> GetAll(int? BranchId)
        {
            if(BranchId==null || BranchId==0)
            {
                var modelList = _unitOfWork.GenericRepository<Room>().GetAll(includeProperties: "RoomBranch,RoomType").Select(x => new RoomDTO(x)).ToList();
                return modelList;
            }

            else {
                var modelList1 = _unitOfWork.GenericRepository<Room>().GetAll(x=>x.RoomBranchId==BranchId,includeProperties: "RoomBranch,RoomType").Select(x => new RoomDTO(x)).ToList();
                return modelList1;
            }
          
        }

        public RoomDTO GetRoomById(int id)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(id);
            model.RoomBranch = _applicationDbContext.Branches.FirstOrDefault(x => x.Id == model.RoomBranchId);
            model.RoomType = _applicationDbContext.RoomTypes.FirstOrDefault(x => x.Id == model.RoomTypeId);
            var dTO = new RoomDTO(model);
          
           
            return dTO;
        }

        //public CreateRoomDTO GetRoomById(int id)
        //{
        //    var model = _unitOfWork.GenericRepository<Room>().GetById(id);
        //    var dTO = new CreateRoomDTO(model);
        //    return dTO;
        //}


        public void InsertRoom(RoomDTO roomDTO)
        {
            var model = new RoomDTO().ConvertViewModel(roomDTO);
            _unitOfWork.GenericRepository<Room>().Add(model);
            _unitOfWork.save();
        }

        public void UpdateRoom(RoomDTO roomDTO)
        {
            var RoomInDb = _unitOfWork.GenericRepository<Room>().GetById(roomDTO.Id);

            RoomInDb.Id = roomDTO.Id;
            RoomInDb.RoomNumber = roomDTO.RoomNumber;
            RoomInDb.price = roomDTO.price;
            RoomInDb.PictureImg = roomDTO.PictureImg;
            RoomInDb.Status = roomDTO.Status;
            RoomInDb.RoomBranchId = roomDTO.RoomBranchId;

            RoomInDb.RoomTypeId = roomDTO.RoomTypeId;

            _unitOfWork.GenericRepository<Room>().Update(RoomInDb);
            _unitOfWork.save();
        }

        public BranchesWithRoomTypesViewModel GetBranchesWRoomTypes ()
        {
            var branchesWromtypes = new BranchesWithRoomTypesViewModel();
           var branches= _unitOfWork.GenericRepository<Branch>().GetAll().Select(x=>new BranchDTO(x)).ToList();
        var roomTypes=    _unitOfWork.GenericRepository<RoomType>().GetAll().Select(x =>new RoomTypeDTO(x)).ToList();
            branchesWromtypes.ListBranches = branches;
            branchesWromtypes.ListRoomTypes = roomTypes; 
            return branchesWromtypes;
        }

        public List<RoomDTO> GetRoomsOfSpecificBranch(string branchName)
        {
            throw new System.NotImplementedException();
        }
    }
}
