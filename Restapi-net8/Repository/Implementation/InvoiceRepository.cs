using Microsoft.EntityFrameworkCore;
using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Implementation;

public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }
    public async Task<IEnumerable<Invoice>> GetAll(){
        return await _dbContext.Invoices.Include(i => i.Status)
                                        .Include(i => i.ShippingStatus)
                                        .Include(i => i.Customer)
                                        .ToListAsync();   
    }
    public async Task<IEnumerable<Invoice>> GetByConditionAsync(Guid id){
        return await _dbContext.Invoices.Where(i => i.CustomerId == id)
                                        .Include(i => i.Status)
                                        .Include(i => i.ShippingStatus)
                                        .Include(i => i.Customer)
                                        .ToListAsync();
    }

    public async Task<Invoice> GetInvoiceId(Guid id)
    {
         return await _dbContext.Invoices
                    .Include(i => i.Status)
                    .Include(i => i.ShippingStatus)
                    .Include(i => i.Customer)
                    .FirstOrDefaultAsync(i => i.Id == id);
    }
}