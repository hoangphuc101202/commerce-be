using System.ComponentModel.DataAnnotations;

public class CreateSupplierRequestDTO{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
    public string name{get;set;}
    public string? logo{get;set;}
    [Required(ErrorMessage = "Contact is required")]
    [Phone(ErrorMessage = "Contact must be a phone number")]
    public string supplierContact {get;set;}
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email must be a valid email address")]
    public string email {get;set;}
    [Required(ErrorMessage = "Phone is required")]
    [Phone(ErrorMessage = "Phone must be a phone number")]
    public string phone {get;set;}
    [Required(ErrorMessage = "Address is required")]
    public string address {get;set;}
}