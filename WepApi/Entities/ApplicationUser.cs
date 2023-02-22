using Microsoft.AspNetCore.Identity;

namespace WepApi.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }    
        public string City { get; set; }    
        public string Address { get; set; } 

    }


}
