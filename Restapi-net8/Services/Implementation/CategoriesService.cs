using AutoMapper;
using Newtonsoft.Json;
using Restapi_net8.Exceptions.Http;
using Restapi_net8.Middlewares;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Category;
using Restapi_net8.Repository.Interface;
using Restapi_net8.Services.Interface;
using Serilog;
using System.Text.Json;

namespace Restapi_net8.Services.Implementation
{
    public class CategoriesService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesService(ICategoryRepository categoryRepository, IMapper mapper) { 
            this.categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateCategory(Category category)
        {
            var categoryCreated = await categoryRepository.CreateAsync(category);
            return new ApiResponse(200, "category created successful", null, null);

        }

        public async Task<ApiResponse> GetAllCategory()
        {
            var categories = await categoryRepository.GetAll();
            var categoriesHandle = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                name = c.Name,
                categoryAliasName = c.CategoryAliasName,
                description = c.Description,
                imageUrl = c.ImageUrl
            }).ToList();
            return new ApiResponse(200, "categories retrieved successful", categoriesHandle, null);
        }
        public async Task<ApiResponse> GetCategoryById(Guid id)
        {
            var category = await categoryRepository.GetById(id);
            if (category == null)
            {
                throw new NotFoundHttpException($"Category with id {id} not found");
            }
            var categoryHandle = new CategoryDTO
            {
                Id = category.Id,
                name = category.Name,
                categoryAliasName = category.CategoryAliasName,
                description = category.Description,
                imageUrl = category.ImageUrl
            };
            return new ApiResponse(200, "category retrieved successful", categoryHandle, null);
        }
        public async Task<ApiResponse> UpdateCategory(UpdateCategoryDTO updateCategory, Guid id)
        {
            var categoryToUpdate = await categoryRepository.GetById(id);
            if (categoryToUpdate == null)
            {
                throw new NotFoundHttpException($"Category with id {id} not found");
            }
            var categoryUpdated = _mapper.Map<Category>(updateCategory);
            categoryUpdated.Id = id;
            await categoryRepository.UpdateAsync(categoryToUpdate,categoryUpdated);
            Log.Debug("Category {0} updated successfully", JsonConvert.SerializeObject(categoryUpdated));
            return new ApiResponse(200, "category updated successful", null, null);
        }
        public async Task<ApiResponse> DeleteCategory(Guid id)
        {
            var categoryToDelete = await categoryRepository.GetById(id);
            if (categoryToDelete == null)
            {
                throw new NotFoundHttpException("Category not found");
            }
            var categoryDeleted = await categoryRepository.SoftDelete(id);
            return new ApiResponse(200, "category deleted successful", null, null);
        }
    }
}
