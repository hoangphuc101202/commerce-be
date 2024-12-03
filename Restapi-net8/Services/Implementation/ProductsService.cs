using System;
using AutoMapper;
using Newtonsoft.Json;
using Restapi_net8.Exceptions.Http;
using Restapi_net8.Middlewares;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Product;
using Restapi_net8.Repository.Interface;
using Restapi_net8.Services.Interface;
using Serilog;

namespace Restapi_net8.Services.Implementation;

public class ProductsService : IProductsService
{   
    private readonly IProductRepository productRepository;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository categoryRepository;
    public ProductsService(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository){
        this.productRepository = productRepository;
        this.categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<ApiResponse> CreateProducts(CreateProductRequestDTO product)
    {
        if(product.categoryId != null)
        {
            var category = await categoryRepository.GetById(Guid.Parse(product.categoryId));
            if(category == null)
            {
                throw new NotFoundHttpException($"Category with id {product.categoryId} not found");
            }
        }
        var productCreatedMap = _mapper.Map<Product>(product);
        productCreatedMap.CategoryId = product.categoryId != null ? Guid.Parse(product.categoryId) : null;
        var productCreated = await productRepository.CreateAsync(productCreatedMap);
        if(productCreated == null)
        {
            throw new NotFoundHttpException("Product cannot created successfully");
        }
        return new ApiResponse(200, "Product created successfully", null, null);
    }

    public async Task<ApiResponse> DeleteProduct(Guid id)
    {
        var product = await productRepository.GetById(id);
        if(product == null)
        {
            throw new NotFoundHttpException($"Product with id {id} not found");
        }
        await productRepository.SoftDelete(product.Id);
        Log.Debug("Product {0} deleted successfully", JsonConvert.SerializeObject(product));
        return new ApiResponse(200, "Product deleted successfully", null, null);
    }

    public async Task<ApiResponse> GetAllProducts(GetAllProductsRequestDTO query)
    {   
        var page = query.page ?? 1;
        var limit = query.limit ?? 12;
        var search = query.search ?? "";
        var sort = query.sort ?? "";
        var products = await productRepository.GetAllProductWithPage(limit, page, search, sort);
        var productHandler = products.Select(p => new ProductDTO
        {
            id = p.Id,
            name = p.Name,
            description = p.Description,
            price = p.Price,
            imageUrl = p.ImageUrl,
            categoryId = p.CategoryId?.ToString(),
            discount = p.Discount,
            productNameAlias = p.ProductNameAlias,
            productDate = p.ProductDate,
            supplierId = p.SupplierId?.ToString(),
            views = p.Views
        }).ToList();
        var totalPageProduct = await productRepository.GetTotalPage(limit);
        if(products == null)
        {
            throw new NotFoundHttpException("Products not found");
        }
        var resultAllProd = new {
            data = productHandler,
            page,
            limit,
            totalPage = totalPageProduct
        };
        return new ApiResponse(200, "Products retrieved successfully", resultAllProd, null);
    }
    public async Task<ApiResponse> UpdateProduct(UpdateProductRequestDTO product, Guid id)
    {
        Log.Debug("Product id {0}", product);
        var productToUpdate = await productRepository.GetById(id);
        if(productToUpdate == null)
        {
            throw new NotFoundHttpException($"Product with id {id} not found");
        }
        
        if(product.categoryId != null)
        {
            Log.Debug("Category id {0}", product.categoryId);
            var category = await categoryRepository.GetById(Guid.Parse(product.categoryId));
            if(category == null)
            {
                throw new NotFoundHttpException($"Category with id {product.categoryId} not found");
            }
        }
        var productUpdated = _mapper.Map<Product>(product);
        productUpdated.Id = id;
        productUpdated.CategoryId = product.categoryId != null ? Guid.Parse(product.categoryId) : null;
        await productRepository.UpdateAsync(productToUpdate, productUpdated);
        Log.Debug("Product {0} updated successfully", JsonConvert.SerializeObject(productUpdated));
        return new ApiResponse(200, "Product updated successfully", null, null);
    }
    public async Task<ApiResponse> GetProductByCategory(string categoryId, GetAllProductsRequestDTO query)
    {
        var page = query.page ?? 1;
        var limit = query.limit ?? 12;
        var search = query.search ?? "";
        var sort = query.sort ?? "";
        var categoryExist = await categoryRepository.GetById(Guid.Parse(categoryId));
        if(categoryExist == null)
        {
            throw new NotFoundHttpException($"Category with id {categoryId} not found");
        }
        var products = await productRepository.GetProductByCategory(Guid.Parse(categoryId), limit, page, search, sort);
        var productHandler = products.Select(p => new ProductDTO
        {
            id = p.Id,
            name = p.Name,
            description = p.Description,
            price = p.Price,
            imageUrl = p.ImageUrl,
            categoryId = p.CategoryId?.ToString(),
            discount = p.Discount,
            productNameAlias = p.ProductNameAlias,
            productDate = p.ProductDate,
            supplierId = p.SupplierId?.ToString(),
            views = p.Views
        }).ToList();
        var totalPageProduct = await productRepository.GetTotalPage(limit);
        if(products == null)
        {
            throw new NotFoundHttpException("Products not found");
        }
        var resultAllProd = new {
            data = productHandler,
            page,
            limit,
            totalPage = totalPageProduct
        };
        return new ApiResponse(200, "Products retrieved successfully", resultAllProd, null);

        
    }
}
