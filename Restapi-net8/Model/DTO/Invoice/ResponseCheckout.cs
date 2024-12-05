using System.Numerics;

public class ResponseCheckout{
    public List<CartItem> cartItems { get; set; }
    public decimal totalAmount { get; set; }
    public decimal shippingFee  { get; set; }
}