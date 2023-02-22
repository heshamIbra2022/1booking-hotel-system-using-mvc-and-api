using System.ComponentModel.DataAnnotations;

namespace WepApi.DTO
{
    public class RegisterDTO
    {
      
        public string UserName { get; set; }
       
        public string Password { get; set; }
     
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
