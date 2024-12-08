using Microsoft.EntityFrameworkCore;
using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Implementation;

public class InvoiceDetailRepository : BaseRepository<InvoiceDetail>, IInvoiceDetailRepository
{
    public InvoiceDetailRepository(ApplicationDBContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<InvoiceDetail>> GetByInvoiceId(Guid id)
    {
        return await _dbContext.InvoiceDetails
            .Include(i => i.Product)
            .Where(i => i.InvoiceId == id)
            .ToListAsync();
    }
}