using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO;
using Restapi_net8.Repository.Interface;
using Restapi_net8.Services.Interface;
using System.Text.Json;

namespace Restapi_net8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryRequestDTO request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            var categoryCreated = await categoryService.CreateCategory(category);
            return Ok(categoryCreated);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await categoryService.GetAllCategory();
            return Ok(categories);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute]Guid id)
        {
            var category = await categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategoryById([FromRoute] Guid id, [FromBody] UpdateCategoryDTO request)
        {
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            var categoryUpdate = await categoryService.UpdateCategory(category, id);
            return Ok(categoryUpdate);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid id)
        {
            var categoryDeleted = await categoryService.DeleteCategory(id);
            return Ok(categoryDeleted);
        }
    }
}
