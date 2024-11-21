using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Product;
using Restapi_net8.Services.Interface;
using Serilog;

namespace Restapi_net8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {   
        private readonly IMapper _mapper;
        private readonly IProductsService productService;
        public ProductsController(IMapper mapper, IProductsService productService)
        {
            _mapper = mapper;
            this.productService = productService;
            
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProductRequestDTO request)
        {  
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productCreated = await productService.CreateProducts(request);
            return Ok(productCreated);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery]GetAllProductsRequestDTO query)
        {
            var products = await productService.GetAllProducts(query);
            return Ok(products);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequestDTO request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _mapper.Map<Product>(request);
            var productUpdated = await productService.UpdateProduct(request, id);
            return Ok(productUpdated);
        }
        [Authorize(Roles= "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var productDeleted = await productService.DeleteProduct(id);
            return Ok(productDeleted);
        }
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetProductByCategory([FromRoute] string categoryId, [FromQuery] GetAllProductsRequestDTO query)
        {
            Log.Debug("Get product by category {0}", categoryId);
            var products = await productService.GetProductByCategory(categoryId, query);
            return Ok(products);
        }
    }
}
