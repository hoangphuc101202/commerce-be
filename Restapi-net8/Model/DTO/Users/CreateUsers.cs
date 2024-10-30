using System.ComponentModel.DataAnnotations;

namespace Restapi_net8.Model.DTO.Users
{
    public class CreateUsers
    {
        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(20, ErrorMessage = "First Name can't be longer than 20 characters")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        [MaxLength(20, ErrorMessage = "Last Name can't be longer than 20 characters")]
        public string lastname { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(50, ErrorMessage = "Email can't be longer than 50 characters")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MaxLength(20, ErrorMessage = "Password can't be longer than 20 characters")]
        public string password { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        [MaxLength(6, ErrorMessage = "Gender can't be longer than 6 characters")]
        public string gender { get; set; }

        public string url { get; set; }
    }
}
