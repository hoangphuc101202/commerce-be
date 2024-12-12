using Restapi_net8.Middlewares;
using Restapi_net8.Model.Domain;

namespace Restapi_net8.Services.Interface
{
    public interface IPaymentService
    {
        Task<ApiResponse> CreatePayment(PaymentRequest paymentRequest);
        Task<ApiResponse> GetAll(GetAllPaymentRequest request);
        Task<ApiResponse> GetPaymentByUser(string userId);
    }
}