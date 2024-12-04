using System.ComponentModel.DataAnnotations;

public class ChangePassword{
    [Required(ErrorMessage = "Old Password is required")]
    public string oldPassword { get; set; }
    [Required(ErrorMessage = "New Password is required")]

    public string newPassword { get; set; }

}