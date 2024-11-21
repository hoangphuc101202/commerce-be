using Microsoft.EntityFrameworkCore;
using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Interface;

namespace Restapi_net8.Repository.Implementation
{
    public class ProductRepository : BaseRepository<Product> ,IProductRepository
    {
        public ProductRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetAllProductWithPage(int limit, int page, string search, string sort)
        {  
            var query = _dbContext.Products.Where(entity => !entity.IsDeleted);

            if(!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(p => 
                p.Name.ToLower().Contains(search) || 
                p.Description.ToLower().Contains(search) ||
                p.ProductNameAlias.ToLower().Contains(search)
                );
                return await query
                            .Skip((page - 1) * limit)
                            .Take(limit)
                            .ToListAsync();
            }
            query = sort?.ToLower() switch{
                "asc" => query.OrderBy(p => p.Price),
                "desc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name)
            };
            return await query
                        .Skip((page - 1) * limit)
                        .Take(limit)
                        .ToListAsync();
        }
        public async Task<int> GetTotalPage(int limit)
        {
            var totalProduct = await _dbContext.Products
                .Where(entity => !entity.IsDeleted)
                .CountAsync();
            return (int)Math.Ceiling((double)totalProduct / limit);
        }
        public async Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId, int limit, int page, string search, string sort)
        {
            var query = _dbContext.Products.Where(entity => !entity.IsDeleted && entity.CategoryId == categoryId);

            if(!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(p => 
                p.Name.ToLower().Contains(search) || 
                p.Description.ToLower().Contains(search) ||
                p.ProductNameAlias.ToLower().Contains(search)
                );
                return await query
                            .Skip((page - 1) * limit)
                            .Take(limit)
                            .ToListAsync();
            }
            query = sort?.ToLower() switch{
                "asc" => query.OrderBy(p => p.Price),
                "desc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name)
            };
            return await query
                        .Skip((page - 1) * limit)
                        .Take(limit)
                        .ToListAsync();
        }
        
    }
}
