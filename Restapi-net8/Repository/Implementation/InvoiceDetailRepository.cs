using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Implementation;

public class InvoiceDetailRepository : BaseRepository<InvoiceDetail>, IInvoiceDetailRepository
{
    public InvoiceDetailRepository(ApplicationDBContext dbContext) : base(dbContext)
    {
    }
}