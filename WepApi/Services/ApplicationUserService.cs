using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using WepApi.ContextModel;
using WepApi.DTO;
using WepApi.Entities;
using WepApi.GenericRepo;

namespace WepApi.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        ApplicationDbContext dbContext;
        UserManager<ApplicationUser> userManager;



        public ApplicationUserService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }



        public ApplicationUser GetUser(int id)
        {
           
         return dbContext.Users.FirstOrDefault(x=>x.Id==id.ToString());
        }

        public List<ApplicationUser> GetUsers()
        {

            var usersInUserRole = (from user in dbContext.Users
                                   join userRole in dbContext.UserRoles
                                   on user.Id equals userRole.UserId
                                   join role in dbContext.Roles
                                   on userRole.RoleId equals role.Id
                                   where role.Name == "User"
                                   select user).ToList();


            return usersInUserRole; 

        }
    }
}
