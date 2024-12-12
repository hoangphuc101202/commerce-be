using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Interface;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    public Task<Payment> GetInvoiceIsPaymentId(Guid id);
    public Task<IEnumerable<Payment>> GetAllPaymentWithPage(int limit, int page, string startDate, string endDate);
    public Task<int> GetTotalPage(int limit);
    public Task<IEnumerable<Payment>> GetPaymentByUser(Guid userId);

}