using WepApi.DTO;
using WepApi.GenericRepo;
using WepApi.Entities;
using System.Collections.Generic;
using System.Linq;

namespace WepApi.Services
{
    public class BranchService : IBranchService
    {

        private IUnitOfWork _unitOfWork;

        public BranchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void DeleteBranch(int id)
        {
            var model = _unitOfWork.GenericRepository<Branch>().GetById(id);
            _unitOfWork.GenericRepository<Branch>().Delete(model);
            _unitOfWork.save();
        }

        public List<BranchDTO> GetAll()
        {
            var modelList = _unitOfWork.GenericRepository<Branch>().GetAll().Select(x => new BranchDTO(x)).ToList();
            return modelList;
        }

        public BranchDTO GetBranchById(int id)
        {
            var model = _unitOfWork.GenericRepository<Branch>().GetById(id);
            var dTO = new BranchDTO(model);
            return dTO;
        }

        public void InsertBranch(BranchDTO branchDTO)
        {
            var model = new BranchDTO().ConvertViewModel(branchDTO);
            _unitOfWork.GenericRepository<Branch>().Add(model);
            _unitOfWork.save();
        }

        public void UpdateBranch(BranchDTO branchDTO)
        {
         // var Branch= new BranchDTO().ConvertViewModel(branchDTO);
            var BranchInDb = _unitOfWork.GenericRepository<Branch>().GetById(branchDTO.Id);
            BranchInDb.Name = branchDTO.Name;
            _unitOfWork.GenericRepository<Branch>().Update(BranchInDb);
            _unitOfWork.save();
        }
    }
}
