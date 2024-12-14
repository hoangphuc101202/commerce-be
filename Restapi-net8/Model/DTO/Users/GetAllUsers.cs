using System.ComponentModel.DataAnnotations;

public class GetAllUsers{
    public string? fullName { get; set; }
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? email { get; set; }
}