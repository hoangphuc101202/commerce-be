using Restapi_net8.Middlewares;

public interface IInvoiceService{
    Task<ApiResponse> Checkout(CheckoutRequest request, string userId);
    Task<ApiResponse> Order(OrderRequest request, string userId);
    Task<ApiResponse> GetStatus();
    Task<ApiResponse> GetShippingStatus();
    Task<ApiResponse> GetAll();
}