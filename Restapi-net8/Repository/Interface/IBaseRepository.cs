namespace Restapi_net8.Repository.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task<TEntity> UpdateAsync(Guid id, TEntity entity);
        Task<TEntity> DeleteAsync(Guid id);
    }
}
