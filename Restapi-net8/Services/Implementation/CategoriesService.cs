using AutoMapper;
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

        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            var categories = await categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
        public async Task<CategoryDTO> GetCategoryById(Guid id)
        {
            var category = await categoryRepository.GetById(id);
            if (category == null)
            {
                throw new NotFoundHttpException($"Category with id {id} not found");
            }
            return _mapper.Map<CategoryDTO>(category);
        }
        public async Task<CategoryDTO> UpdateCategory(Category category, Guid id)
        {
            var categoryToUpdate = await categoryRepository.GetById(id);
            if (categoryToUpdate == null)
            {
                throw new NotFoundHttpException($"Category with id {id} not found");
            }
            var categoryUpdated = await categoryRepository.UpdateAsync(categoryToUpdate.Id, category);
            return _mapper.Map<CategoryDTO>(categoryUpdated);
        }
        public async Task<CategoryDTO> DeleteCategory(Guid id)
        {
            var categoryToDelete = await categoryRepository.GetById(id);
            if (categoryToDelete == null)
            {
                throw new NotFoundHttpException("Category not found");
            }
            var categoryDeleted = await categoryRepository.SoftDelete(id);
            return _mapper.Map<CategoryDTO>(categoryDeleted);
        }
    }
}
