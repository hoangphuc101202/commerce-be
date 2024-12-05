using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Implementation;

public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

}