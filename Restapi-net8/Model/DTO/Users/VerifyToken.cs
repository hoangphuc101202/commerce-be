using System.ComponentModel.DataAnnotations;

public class VerifyToken{
    public enum VerifyEmail
    {
        [Display(Name = "Verify Email")]
        True
    }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string email { get; set; }
    [Required(ErrorMessage = "Token is required")]
    public string token { get; set; }
    [EnumDataType(typeof(VerifyEmail))]
    public string? verifyEmail { get; set; }
}