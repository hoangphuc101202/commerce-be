using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO;

namespace Restapi_net8.Services.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<CategoryDTO> CreateCategory(Category category);
        Task<CategoryDTO> GetCategoryById(Guid id);
        Task <CategoryDTO> UpdateCategory(Category category, Guid id);
        Task<CategoryDTO> DeleteCategory(Guid id);
    }
}
