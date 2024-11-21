using Restapi_net8.Model.Domain;

namespace Restapi_net8.Repository.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : BaseDomain
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task<TEntity> UpdateAsync(TEntity exist,TEntity entity);
        Task<TEntity> DeleteAsync(Guid id);
        Task<TEntity> SoftDelete(Guid id);

    }
}
