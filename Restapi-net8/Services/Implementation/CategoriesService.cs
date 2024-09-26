using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO;
using Restapi_net8.Repository.Interface;
using Restapi_net8.Services.Interface;

namespace Restapi_net8.Services.Implementation
{
    public class CategoriesService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoriesService(ICategoryRepository categoryRepository) { 
            this.categoryRepository = categoryRepository;
        }

        public async Task<CategoryDTO> CreateCategory(Category category)
        {
            var categoryCreated = await categoryRepository.CreateAsync(category);
            return new CategoryDTO
            {
                Id = categoryCreated.Id,
                Name = categoryCreated.Name,
                UrlHandle = categoryCreated.UrlHandle
            };
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            var categories = await categoryRepository.GetAllCategory();
            return categories.Select(category => new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            });
        }
        public async Task<CategoryDTO> GetCategoryById(Guid id)
        {
            var category = await categoryRepository.GetById(id);
            if (category == null)
            {
                return null;
            }
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
        }
        public async Task<CategoryDTO> UpdateCategory(Category category, Guid id)
        {
            var categoryToUpdate = await categoryRepository.GetById(id);
            if (categoryToUpdate == null)
            {
                throw new Exception("Category not found");
            }
            var categoryUpdated = await categoryRepository.UpdateAsync(categoryToUpdate.Id, category);
            return new CategoryDTO
            {
                Id = categoryUpdated.Id,
                Name = categoryUpdated.Name,
                UrlHandle = categoryUpdated.UrlHandle
            };
        }
        public async Task<CategoryDTO> DeleteCategory(Guid id)
        {
            var categoryToDelete = await categoryRepository.GetById(id);
            if (categoryToDelete == null)
            {
                throw new Exception("Category not found");
            }
            var categoryDeleted = await categoryRepository.DeleteAsync(id);
            return new CategoryDTO
            {
                Id = categoryDeleted.Id,
                Name = categoryDeleted.Name,
                UrlHandle = categoryDeleted.UrlHandle
            };
        }
    }
}
