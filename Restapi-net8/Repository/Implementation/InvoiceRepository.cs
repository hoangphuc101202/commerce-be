using System.Globalization;
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
    public async Task<IEnumerable<Invoice>> GetAllInvoiceWithPage(int limit, int page, string startDate, string endDate, string statusShipping)
    {
        var query = _dbContext.Invoices.Include(i => i.Status)
                                        .Include(i => i.ShippingStatus)
                                        .Include(i => i.Customer)
                                        .AsQueryable();
        if (!string.IsNullOrEmpty(startDate))
        {
            var parsedStartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", 
                CultureInfo.InvariantCulture);
            query = query.Where(i => i.OrderDate >= parsedStartDate);
        }

        if (!string.IsNullOrEmpty(endDate))
        {
            var parsedEndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", 
                CultureInfo.InvariantCulture)
                .AddDays(1) 
                .AddSeconds(-1); 
            query = query.Where(i => i.OrderDate <= parsedEndDate);
        }
        if (!string.IsNullOrEmpty(statusShipping))
        {
            query = query.Where(i => i.ShippingStatus.Name == statusShipping);
        }
        return await query.Skip((page - 1) * limit).Take(limit).ToListAsync();
    }
}