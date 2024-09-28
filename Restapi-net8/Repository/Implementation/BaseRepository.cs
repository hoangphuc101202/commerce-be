using Microsoft.EntityFrameworkCore;
using Restapi_net8.Data;
using Restapi_net8.Repository.Interface;

namespace Restapi_net8.Repository.Implementation
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDBContext _dbContext;
        protected BaseRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(Guid id)
        {
            var entityToDelete = await _dbContext.Set<TEntity>().FindAsync(id);
            if (entityToDelete == null)
            {
                throw new Exception($"{typeof(TEntity).Name} not found");
            }
            _dbContext.Set<TEntity>().Remove(entityToDelete);
            await _dbContext.SaveChangesAsync();
            return entityToDelete;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().Where(x => (bool)x.GetType().GetProperty("IsDeleted").GetValue(x) == false).ToListAsync();
        }
        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity> UpdateAsync(Guid id, TEntity entity)
        {
            var entityToUpdate = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entityToUpdate == null)
            {
                throw new Exception($"{typeof(TEntity).Name} not found");
            }
            _dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);

            await _dbContext.SaveChangesAsync();

            return entityToUpdate;
        }
        public async Task<TEntity> SoftDelete(Guid id)
        {
            var entityToDelete = await _dbContext.Set<TEntity>().FindAsync(id);
            if (entityToDelete == null)
            {
                throw new Exception($"{typeof(TEntity).Name} not found");
            }
            var property = entityToDelete.GetType().GetProperty("IsDeleted");
            if (property != null)
            {
                property.SetValue(entityToDelete, true);
                _dbContext.Entry(entityToDelete).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return entityToDelete;
            }
            throw new Exception("Entity does not have IsDeleted property");
        }
    }
}
