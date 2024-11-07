using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Category;
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
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryRequestDTO request)
        {  
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = _mapper.Map<Category>(request);
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
                ImageUrl = request.UrlHandle,
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
