using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WepApi.DTO;
using WepApi.Entities;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration Configuration)
        {
            this.userManager = userManager;
            configuration = Configuration;
        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
          
            ApplicationUser userModel = new ApplicationUser();
            userModel.UserName = registerDto.UserName;
            userModel.Email = registerDto.Email;
            userModel.Address = registerDto.Address;
            userModel.City = registerDto.City;

            IdentityResult result = await userManager.CreateAsync(userModel, registerDto.Password);
            IdentityResult roleResult = await userManager.AddToRoleAsync(userModel, "User");
            if (result.Succeeded)
            {
                var mytoken = await GenerateToke(userModel);
                return Ok(

                    new JwtSecurityTokenHandler().WriteToken(mytoken)

                );
            }
            else
            {
                
                foreach (var item in result.Errors)
                {
                   
                    ModelState.AddModelError("", item.Description);
                }
                 return BadRequest(ModelState);
             
            }
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDTO registerDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser userModel = new ApplicationUser();
            userModel.UserName = registerDto.UserName;
            userModel.Email = registerDto.Email;
            userModel.Address = registerDto.Address;
            userModel.City = registerDto.City;

            IdentityResult result = await userManager.CreateAsync(userModel, registerDto.Password);
            IdentityResult roleResult = await userManager.AddToRoleAsync(userModel, "Admin");
            if (result.Succeeded)
            {
                return Ok("success");
            }
            else
            {
              
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
           
            ApplicationUser userModel = await userManager.FindByNameAsync(loginDto.UserNAme);
            if (userModel != null)
            {
                if (await userManager.CheckPasswordAsync(userModel, loginDto.Password) == true)
                {
                   
                    var mytoken = await GenerateToke(userModel);
                    return Ok(
                    
                        new JwtSecurityTokenHandler().WriteToken(mytoken)
                       
                    );
                }
                else
                {
                   
                    return Unauthorized();//401
                }
            }
            return Unauthorized();
        }
        [NonAction]
        public async Task<JwtSecurityToken> GenerateToke(ApplicationUser userModel)
        {
            var claims = new List<Claim>();
           
            claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id));
            var roles = await userManager.GetRolesAsync(userModel);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
           
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
          
            var key =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecrtKey"]));
            var mytoken = new JwtSecurityToken(
                audience: configuration["JWT:ValidAudience"],
                issuer: configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials:
                       new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return mytoken;
        }
    }

}

