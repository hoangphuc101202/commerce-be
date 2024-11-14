using Restapi_net8.Middlewares;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Category;

namespace Restapi_net8.Services.Interface
{
    public interface ICategoryService
    {
        Task<ApiResponse> GetAllCategory();
        Task<ApiResponse> CreateCategory(Category category);
        Task<ApiResponse> GetCategoryById(Guid id);
        Task <ApiResponse> UpdateCategory(UpdateCategoryDTO updateCategoryDTO, Guid id);
        Task<ApiResponse> DeleteCategory(Guid id);
    }
}
