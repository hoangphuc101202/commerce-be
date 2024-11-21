using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restapi_net8.Model.DTO.Product;

public class GetAllProductsRequestDTO
{
    public enum SortType
    {
        asc,
        desc
    }
    public int? page { get; set; } 
    public int? limit { get; set; } 
    public string? search { get; set; }
    [EnumDataType(typeof(SortType), ErrorMessage = "Sort must be either 'asc' or 'desc'")]
    public string? sort { get; set; } 
}
