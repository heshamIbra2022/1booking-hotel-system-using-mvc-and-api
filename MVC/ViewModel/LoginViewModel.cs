using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModel
{
    public class LoginViewModel
    {
        public string UserNAme { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
