using System.ComponentModel.DataAnnotations;

public class CreateInvoiceeDetails
{
    [Required (ErrorMessage = "Price is required")]
    public float price { get; set; }
    [Required (ErrorMessage = "Quantity is required")]
    public float quantity { get; set; }
    [Required (ErrorMessage = "Product id is required")]
    public string productId { get; set; }
}