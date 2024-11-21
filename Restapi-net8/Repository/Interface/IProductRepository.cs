using System;
using Restapi_net8.Model.Domain;

namespace Restapi_net8.Repository.Interface;

public interface IProductRepository : IBaseRepository<Product>
{
    public Task<IEnumerable<Product>> GetAllProductWithPage(int limit, int page, string search, string sort);
    public Task<int> GetTotalPage(int limit);
    public Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId, int limit, int page, string search, string sort);
}
