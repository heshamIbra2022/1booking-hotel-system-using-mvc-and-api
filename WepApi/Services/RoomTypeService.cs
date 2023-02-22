using System.Collections.Generic;
using System.Linq;
using WepApi.DTO;
using WepApi.Entities;
using WepApi.GenericRepo;

namespace WepApi.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private IUnitOfWork _unitOfWork;

        public RoomTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteRoomType(int id)
        {
            var model = _unitOfWork.GenericRepository<RoomType>().GetById(id);
            _unitOfWork.GenericRepository<RoomType>().Delete(model);
            _unitOfWork.save();
        }

        public List<RoomTypeDTO> GetAll()
        {
            var modelList = _unitOfWork.GenericRepository<RoomType>().GetAll().Select(x => new RoomTypeDTO(x)).ToList();
            return modelList;
        }

        public RoomTypeDTO GetRoomTypeById(int id)
        {
            var model = _unitOfWork.GenericRepository<RoomType>().GetById(id);
            var dTO = new RoomTypeDTO(model);
            return dTO;
        }

        public void InsertRoomType(RoomTypeDTO roomTypeDTO)
        {
            var model = new RoomTypeDTO().ConvertViewModel(roomTypeDTO);
            _unitOfWork.GenericRepository<RoomType>().Add(model);
            _unitOfWork.save();
        }

        public void UpdateRoomType(RoomTypeDTO roomTypeDTO)
        {
            var RoomType= new RoomTypeDTO().ConvertViewModel(roomTypeDTO);
            var RoomTypeInDb = _unitOfWork.GenericRepository<RoomType>().GetById(roomTypeDTO.Id);
            RoomTypeInDb.Name = roomTypeDTO.Name;
            _unitOfWork.GenericRepository<RoomType>().Update(RoomTypeInDb);
            _unitOfWork.save();
        }
    }
}
