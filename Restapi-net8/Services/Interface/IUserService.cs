using Restapi_net8.Middlewares;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Users;

namespace Restapi_net8.Services.Interface
{
    public interface IUserService
    {
        Task<ApiResponse> CreateUserService(CreateUsers request);
        Task<ApiResponse> LoginUserService(LoginUser request);

        Task<ApiResponse> RefreshTokenService(RefreshToken request);
        Task<ApiResponse> UserInfoService(string userId);
        Task<ApiResponse>LogoutService(string userId);
        Task<ApiResponse>UpdateUserService(UpdateUsers request, string userId);
        Task<ApiResponse>ForgotPasswordService(ForgotPassword request);
        Task<ApiResponse>VerifyTokenService(VerifyToken request);
        Task<ApiResponse>ResetPasswordService(ResetPassword request);
    }
}
