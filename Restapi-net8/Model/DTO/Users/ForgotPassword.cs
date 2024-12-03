using System.ComponentModel.DataAnnotations;

public class ForgotPassword
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string email { get; set; }
}