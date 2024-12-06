using System.ComponentModel.DataAnnotations;

public class OrderRequest{
    [Required(ErrorMessage = "The field address is required")]
    public string address { get; set; }
    [Required(ErrorMessage = "The field payment method is required")]
    public string paymentMethod { get; set; }
    public string? note { get; set; }
    public List<CartItem> cartItems { get; set; }
}