using System;
using Restapi_net8.Middlewares;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Product;

namespace Restapi_net8.Services.Interface;

public interface IProductsService
{
    Task<ApiResponse> CreateProducts(CreateProductRequestDTO product);
    Task<ApiResponse> GetAllProducts(GetAllProductsRequestDTO query);
    Task<ApiResponse> UpdateProduct(UpdateProductRequestDTO product, Guid id);
    Task<ApiResponse> DeleteProduct(Guid id);
    Task<ApiResponse> GetProductByCategory(string categoryId, GetAllProductsRequestDTO query);
}
