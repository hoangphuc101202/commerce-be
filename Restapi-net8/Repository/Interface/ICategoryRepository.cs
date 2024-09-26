using Restapi_net8.Model.Domain;

namespace Restapi_net8.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllCategory();
        Task<Category?> GetById(Guid id);
        Task<Category> UpdateAsync(Guid id, Category category);
        Task<Category> DeleteAsync(Guid id);
    }
}
