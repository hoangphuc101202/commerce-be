using System.ComponentModel.DataAnnotations;

namespace Restapi_net8.Model.DTO.Users
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(50, ErrorMessage = "Email can't be longer than 50 characters")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}
