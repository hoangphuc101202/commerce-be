
using System.ComponentModel.DataAnnotations;

public class RefreshToken
{
    [Required(ErrorMessage = "Refresh Token is Required")]
    public string refreshToken { get; set; }
   
}