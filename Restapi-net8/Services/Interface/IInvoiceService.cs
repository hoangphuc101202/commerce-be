using Restapi_net8.Middlewares;

public interface IInvoiceService{
    Task<ApiResponse> Checkout(CheckoutRequest request, string userId);
    Task<ApiResponse> Order(OrderRequest request, string userId);
    Task<ApiResponse> GetStatus();
    Task<ApiResponse> GetShippingStatus();
    Task<ApiResponse> GetAll(GetAllInvoiceRequest request);
    Task<ApiResponse> getOrderOfUser(Guid id);
    Task<ApiResponse> GetInvoice(string id, string role, string userId);
    Task<ApiResponse> UpdateInvoiceForAdmin(UpdateInvoiceRequest request, string id);
    Task<ApiResponse> UpdateInvoiceForUser(UpdateInvoiceRequestForUser request, string id, string userId);
}