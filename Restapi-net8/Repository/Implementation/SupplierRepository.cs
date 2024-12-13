using Microsoft.EntityFrameworkCore;
using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Implementation;

public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(ApplicationDBContext context) : base(context)
    {
    }
    public async Task<Supplier> GetByNameAsync(string name)
    {
        return await _dbContext.Suppliers.FirstOrDefaultAsync(s => s.Name == name && s.IsDeleted == false);
    }
    public async Task<Supplier> GetByEmailAsync(string email)
    {
        return await _dbContext.Suppliers.FirstOrDefaultAsync(s => s.Email == email && s.IsDeleted == false);
    }
}