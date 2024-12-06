using System.ComponentModel.DataAnnotations;
public enum PaymentMethod
{
    Cash = 0,
    Banking = 1
}
public class OrderRequest{
    [Required(ErrorMessage = "The field address is required")]
    public string address { get; set; }
    [Required(ErrorMessage = "The field payment method is required")]
    [EnumDataType(typeof(PaymentMethod), ErrorMessage = "Payment method must be 0 (Cash) or 1 (Banking)")]
    public int paymentMethod { get; set; }
    public string? note { get; set; }
    [Required(ErrorMessage = "The field total amount is required")]
    public decimal totalAmount { get; set; }
    public List<CartItem> cartItems { get; set; }
}