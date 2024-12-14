using System.ComponentModel.DataAnnotations;

public class UpdateRole{
    public enum UserRole
    {
        Admin,
        Customer
    }
    [EnumDataType(typeof(UserRole))]
    public string? role { get; set; }
}