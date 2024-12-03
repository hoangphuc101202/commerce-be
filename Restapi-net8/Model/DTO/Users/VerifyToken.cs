using System.ComponentModel.DataAnnotations;

public class VerifyToken{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string email { get; set; }
    [Required(ErrorMessage = "Token is required")]
    public string token { get; set; }
}