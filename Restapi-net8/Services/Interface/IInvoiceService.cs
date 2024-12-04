using Restapi_net8.Middlewares;

public interface IInvoiceService{
    Task<ApiResponse> CreateInvoice(CreateInvoice request, string userId);
}