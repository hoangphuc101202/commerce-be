using Microsoft.Identity.Client;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Interface;

public interface IInvoiceRepository : IBaseRepository<Invoice>
{
    public Task<IEnumerable<Invoice>> GetAllInvoiceWithPage(int limit, int page, string startDate,  string endDate, string statusShipping);
    public Task<IEnumerable<Invoice>> GetByConditionAsync(Guid id );
    public Task<Invoice> GetInvoiceId(Guid id);
}