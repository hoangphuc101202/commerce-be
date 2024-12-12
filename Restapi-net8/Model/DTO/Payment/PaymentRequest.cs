using System.ComponentModel.DataAnnotations;

public class PaymentRequest {
    [Required(ErrorMessage = "Invoice Id is required")]
    public string invoiceId { get; set; }
}