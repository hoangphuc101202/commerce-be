using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Interface;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    public Task<Supplier>GetByNameAsync(string name);
    public Task<Supplier> GetByEmailAsync(string phone);
}