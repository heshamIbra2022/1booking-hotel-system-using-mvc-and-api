using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using WepApi.DTO;
using WepApi.Services;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private IBranchService _branchService;
        
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet("getAllBranches")]
        public IActionResult Index()
        {
       
          var branches=  _branchService.GetAll();
            if (branches == null)
            {
                return NotFound();
            }
            return Ok(branches);
        }

        [HttpPost("addnewBranch")]
        public IActionResult Create(BranchDTO branchDTO)
        {

            _branchService.InsertBranch(branchDTO); 
            return Ok(branchDTO);

        }
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try { 
         var branch=   _branchService.GetBranchById(id);
            return Ok(branch);
            }catch
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IActionResult Edit(BranchDTO branchDTO)
        {
            if(ModelState.IsValid==true)
            {
                _branchService.UpdateBranch(branchDTO);
                return Ok(branchDTO);
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteBranch(int id)
        {
            try {
                _branchService.DeleteBranch(id);
                return Ok("deleted");
            }
            catch
            {
                return BadRequest();
            }
           
        }
    }
}
