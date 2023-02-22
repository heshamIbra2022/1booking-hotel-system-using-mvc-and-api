using System.Collections.Generic;
using WepApi.DTO;
using WepApi.Entities;

namespace WepApi.Services
{
    public interface IApplicationUserService
    {
        List<ApplicationUser> GetUsers();
        ApplicationUser GetUser(int id);
    }
}
