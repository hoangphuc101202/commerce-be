using System.ComponentModel.DataAnnotations;

public class ResetPassword{
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string email { get; set; }
    public string password { get; set; }
}