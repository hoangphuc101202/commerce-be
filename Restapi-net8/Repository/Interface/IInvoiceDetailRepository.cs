using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Interface;

public interface IInvoiceDetailRepository : IBaseRepository<InvoiceDetail>
{
    public Task<IEnumerable<InvoiceDetail>>GetByInvoiceId(Guid id);
}