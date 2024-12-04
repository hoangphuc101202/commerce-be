using System.ComponentModel.DataAnnotations;

public class CreateInvoice{
    [Required (ErrorMessage = "Address is required")]
    public string address { get; set; }
    [Required (ErrorMessage = "Payment method is required")]
    public string paymentMethod { get; set; }
    [Required (ErrorMessage = "Shipping fee is required")]
    public float shippingFee { get; set; }
    [Required (ErrorMessage = "Status id is required")]
    public string statusId { get; set; }
    public string? note { get; set; }
    public List<CreateInvoiceeDetails> invoiceDetails { get; set; }

}