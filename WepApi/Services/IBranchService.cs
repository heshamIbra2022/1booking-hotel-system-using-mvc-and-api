using System.Collections.Generic;
using WepApi.DTO;

namespace WepApi.Services
{
    public interface IBranchService
    {
      List<BranchDTO> GetAll();
        BranchDTO GetBranchById(int id);
        void UpdateBranch(BranchDTO branchDTO);
        void DeleteBranch(int id);
        void InsertBranch(BranchDTO branchDTO);
    }
}
