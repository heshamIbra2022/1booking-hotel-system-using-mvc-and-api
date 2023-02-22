using System.Collections.Generic;
using WepApi.Entities;

namespace WepApi.DTO
{
    public class BranchDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BranchDTO()
        {

        }
        public BranchDTO(Branch branch)
        {
            Id = branch.Id;
            Name = branch.Name;

        }

        public Branch ConvertViewModel(BranchDTO branchDTO)
        {
            return new Branch
            {
               Id= (int)branchDTO.Id,
               Name = branchDTO.Name,   
            };



        }
    }
}
