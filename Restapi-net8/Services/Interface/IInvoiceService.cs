using Restapi_net8.Middlewares;

public interface IInvoiceService{
    Task<ApiResponse> Checkout(CheckoutRequest request, string userId);
}