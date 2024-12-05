using System.ComponentModel.DataAnnotations;

public class CartItem{
    [Required(ErrorMessage = "The field productId is required")]
    public string productId { get; set; }
    [Required(ErrorMessage = "The field quantity is required")]
    public int quantity { get; set; }
    public decimal? discount { get; set; }
    public string? productName { get; set; }
}