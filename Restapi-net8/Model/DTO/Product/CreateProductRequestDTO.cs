using System.ComponentModel.DataAnnotations;

public class CreateProductRequestDTO
{
    [MaxLength(100, ErrorMessage = "must not exceed 50 characters. ")]
    [Required(ErrorMessage = "name is required. ")]
    public string name { get; set; }
    [MaxLength(50, ErrorMessage = "must not exceed 50 characters. ")]
    public string? productNameAlias { get; set; }

    public string? categoryId { get; set; }
    public float price { get; set; }
    public string? imageUrl { get; set; }
    public string? productDate { get; set; }
    public float? discount { get; set; }
    public long? views { get; set;  }
    [MaxLength(300, ErrorMessage = "must not exceed 300 characters. ")]
    public string? description { get; set; }
    public string? supplierId { get; set; }
}