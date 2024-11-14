using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Interface;
using Serilog;

namespace Restapi_net8.Repository.Implementation
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseDomain
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
            return await _dbContext.Set<TEntity>().Where(entity => !entity.IsDeleted).ToListAsync();
        }
        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbContext.Set<TEntity>()
                          .Where(entity => !entity.IsDeleted && entity.Id == id)
                          .FirstOrDefaultAsync();
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
            throw new Exception("Entity does not have is_deleted property");
        }

        public async Task<TEntity> UpdateAsync(TEntity entityToUpdate,TEntity entity)
        {
             foreach (var property in typeof(TEntity).GetProperties())
            {
                if (property.Name == "Id")
                {
                    continue;
                }

                var newValue = property.GetValue(entity);
                var currentValue = property.GetValue(entityToUpdate);
                if (newValue != null && !newValue.Equals(currentValue))
                {
                    property.SetValue(entityToUpdate, newValue);
                    _dbContext.Entry(entityToUpdate).Property(property.Name).IsModified = true;
                }
            }

            await _dbContext.SaveChangesAsync();

            return entity;
        }
       
    }
}
