using System;
using System.ComponentModel.DataAnnotations;

namespace Restapi_net8.Model.DTO.Product;

public class UpdateProductRequestDTO
{
    [MaxLength(100, ErrorMessage = "must not exceed 50 characters. ")]
    [Required(ErrorMessage = "name is required. ")]
    [RegularExpression(@"^[\p{L}\p{N}\s]*$", ErrorMessage = "name must be alphanumeric. ")]
    public string name { get; set; }
    [RegularExpression(@"^[\p{L}\p{N}\s]*$", ErrorMessage = "product alias name must be alphanumeric. ")]
    [MaxLength(50, ErrorMessage = "must not exceed 50 characters. ")]
    public string? productNameAlias { get; set; }
    public string? categoryId { get; set; }
    public float price { get; set; }
    public string? imageUrl { get; set; }
    public string? productDate { get; set; }
    public float? discount { get; set; }
    [RegularExpression(@"^[\p{L}\p{N}\s]*$", ErrorMessage = "name must be alphanumeric. ")]
    [MaxLength(300, ErrorMessage = "must not exceed 300 characters. ")]
    public string? description { get; set; }
    public string? supplierId { get; set; }
}
