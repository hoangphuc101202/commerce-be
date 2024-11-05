using Restapi_net8.Middlewares;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Category;

namespace Restapi_net8.Services.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<ApiResponse> CreateCategory(Category category);
        Task<CategoryDTO> GetCategoryById(Guid id);
        Task <CategoryDTO> UpdateCategory(Category category, Guid id);
        Task<CategoryDTO> DeleteCategory(Guid id);
    }
}
