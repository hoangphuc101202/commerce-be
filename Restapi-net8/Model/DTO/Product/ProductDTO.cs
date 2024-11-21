using System;

namespace Restapi_net8.Model.DTO.Product;

public class ProductDTO
{
    public Guid id { get; set; }
    public string name { get; set; }
    public string? productNameAlias { get; set; }
    public string? categoryId { get; set; }
    public float? price { get; set; }
    public string? imageUrl { get; set; }
    public DateTime? productDate { get; set; }
    public float? discount { get; set; }
    public long? views { get; set; }
    public string? description { get; set; }
    public string? supplierId { get; set; }
}
