using Restapi_net8.Middlewares;

public class InvoiceService : IInvoiceService
{
    public async Task<ApiResponse> CreateInvoice(CreateInvoice request, string userId)
    {
        
        return new ApiResponse(200, "Inovoice created successfully", null, null);
    }
}