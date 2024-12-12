using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Implementation;

public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(ApplicationDBContext context) : base(context)
    {
        
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentWithPage(int limit, int page, string startDate, string endDate)
    {
        var query = _dbContext.Payments.Include(i => i.Customer)       
                                        .AsQueryable();
        if (!string.IsNullOrEmpty(startDate))
        {
            var parsedStartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", 
                CultureInfo.InvariantCulture);
            query = query.Where(i => i.PaymentDate >= parsedStartDate);
        }

        if (!string.IsNullOrEmpty(endDate))
        {
            var parsedEndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", 
                CultureInfo.InvariantCulture)
                .AddDays(1) 
                .AddSeconds(-1); 
            query = query.Where(i => i.PaymentDate <= parsedEndDate);
        }
        return await query.Skip((page - 1) * limit).Take(limit).ToListAsync();
    }

    public async Task<Payment> GetInvoiceIsPaymentId(Guid id)
    {
        return await _dbContext.Payments.FirstOrDefaultAsync(x => x.InvoiceId == id);
    }
    public async Task<int> GetTotalPage(int limit)
    {
        var total = await _dbContext.Payments.CountAsync();
        return (int)Math.Ceiling((double)total / limit);
    }
    public async Task<IEnumerable<Payment>> GetPaymentByUser(Guid userId)
    {
        return await _dbContext.Payments.Where(x => x.CustomerId == userId).ToListAsync();
    }
}