using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Restapi_net8.Exceptions.Http;
using Restapi_net8.Infrastructure.Authentication;
using Restapi_net8.Infrastructure.Password;
using Restapi_net8.Middlewares;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Users;
using Restapi_net8.Repository.Interface;
using Restapi_net8.Services.Interface;
using Serilog;

namespace Restapi_net8.Services.Implementation
{
    public class UserService : IUserService
    {   
        private readonly IUsersRepository _usersRepository;
        private readonly PasswordHasher _passwordHasher;
        private readonly TokenProvider _tokenProvider;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        private readonly MailService _mailService;
        public UserService(IUsersRepository usersRepository, PasswordHasher passwordHasher, IMapper mapper, TokenProvider tokenProvider, IDistributedCache distributedCache, MailService mailService)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _tokenProvider = tokenProvider;
            _distributedCache = distributedCache;
            _mailService = mailService;
        }
        private IDictionary<string, object> DecodeJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Giải mã token mà không cần xác minh chữ ký
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Lấy payload từ token (các claims)
            var claims = jwtToken.Claims.ToDictionary(claim => claim.Type, claim => (object)claim.Value);

            return claims;
        }
        public async Task<ApiResponse> CreateUserService(CreateUsers request)
        {
            var email = request.email;
            var password = request.password;
            var emailExist = await _usersRepository.GetEmailUser(email);
            if (emailExist != null)
            {
                throw new BadRequestHttpException("Email already exist");
            }
            var fullName = request.lastname + " " + request.firstname;
            var hashedPassword = _passwordHasher.HashPassword(password);
            var customer = _mapper.Map<Customer>(request);
            customer.Password = hashedPassword;
            customer.FullName = fullName;
            customer.IsActive = true;
            customer.Role = "Customer";
            customer.Gender = request.gender;
            customer.ImageUrl = request.url;
            await _usersRepository.CreateAsync(customer);
            Log.Debug("User {0} created successfully", customer.Email);
            return new ApiResponse(200, "User created successfully", null, null);

        }

        public async Task<ApiResponse> LoginUserService(LoginUser request)
        {
            var email = request.email;
            var userExist = await _usersRepository.GetEmailUser(email);
            if(userExist == null)
            {
                throw new BadRequestHttpException("Email not found");
            }
            if (!_passwordHasher.VerifyPassword(request.password, userExist.Password))
            {
                throw new BadRequestHttpException("Password is incorrect");
            }
            var payload = new PayloadUsers{
                IdUser = userExist.Id.ToString(),
                Email = userExist.Email,
                FullName = userExist.FullName,
                Role = userExist.Role
            };
            var (accessToken, refreshToken) =  _tokenProvider.GenerateJsonWebToken(payload);            
             var cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
            };
            await _distributedCache.SetStringAsync(userExist.Id.ToString(), refreshToken, cacheEntryOptions);
            var token = new {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return new ApiResponse(200, "Login successful", token, null);
        }

        public async Task<ApiResponse> RefreshTokenService(RefreshToken request)
        {
            var refreshToken = request.refreshToken;
            var payload = DecodeJwtToken(refreshToken);
            var userPayload = new PayloadUsers 
            {
                IdUser = payload.ContainsKey("sub") ? payload["sub"].ToString() : null,
                Email = payload.ContainsKey("email") ? payload["email"].ToString() : null,
                FullName = payload.ContainsKey("FullName") ? payload["FullName"].ToString() : null,
                Role = payload.ContainsKey(ClaimTypes.Role) ? payload[ClaimTypes.Role].ToString(): null
            };
            var refreshTokenExist = await  _distributedCache.GetStringAsync(userPayload.IdUser);
            if (!refreshToken.Equals(refreshTokenExist, StringComparison.OrdinalIgnoreCase))
            {
                throw new BadRequestHttpException("Invalid refresh token");
            }
            
            var (accessToken, newRefreshToken) = _tokenProvider.GenerateJsonWebToken(userPayload);
            var cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
            };
            await _distributedCache.SetStringAsync(userPayload.IdUser, newRefreshToken, cacheEntryOptions);
            var token = new {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
            return new ApiResponse(200, "Generate New Refresh Token successful", token, null);

        }

        public async Task<ApiResponse> UserInfoService(string userId)
        {
            var userExist = await _usersRepository.GetById(Guid.Parse(userId));
            if (userExist == null)
            {
                throw new BadRequestHttpException("User not found");
            }
            var user = _mapper.Map<UserInfo>(userExist);
            Log.Debug("Get user info successful {0}", JsonConvert
                .SerializeObject(user));
            return new ApiResponse(200, "Get user info successful", user, null);
        }
        public async Task<ApiResponse> LogoutService(string userId)
        {
            var refreshTokenExist = await _distributedCache.GetStringAsync(userId);
            if (refreshTokenExist == null)
            {
                throw new BadRequestHttpException("User not found");
            }
            await _distributedCache.RemoveAsync(userId);
            return new ApiResponse(200, "Logout successful", null, null);
        }
        public async Task<ApiResponse> UpdateUserService(UpdateUsers request, string userId)
        {
            var userExist = await _usersRepository.GetById(Guid.Parse(userId));
            if (userExist == null)
            {
                throw new BadRequestHttpException("User not found");
            }
            var userUpdate = _mapper.Map<Customer>(request);
            // userUpdate.BirthOfDate = DateTime.Parse(request.birthOfDate);
            userUpdate.Id = Guid.Parse(userId);
            Log.Debug("Update user {0}", JsonConvert.SerializeObject(userUpdate));
            var userUpdated = await _usersRepository.UpdateAsync(userExist,userUpdate);
            return new ApiResponse(200, "Update user successful", null, null);
        }
        public async Task<ApiResponse> ForgotPasswordService(ForgotPassword request)
        {
            var email = request.email;
            var userExist = await _usersRepository.GetEmailUser(email);
            if (userExist == null)
            {
                throw new BadRequestHttpException("Email not found");
            }
            var tokenExist = await _distributedCache.GetStringAsync($"TokenForgetPassword:{email}");
            if (tokenExist != null)
            {
                await _distributedCache.RemoveAsync($"TokenForgetPassword:{email}");
            }
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            var cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            await _distributedCache.SetStringAsync($"TokenForgetPassword:{email}", GuidString, cacheEntryOptions);
            var mailResponse = await _mailService.SendMail(email, GuidString);
            return new ApiResponse(200, mailResponse, null, null);
        }

        public async Task<ApiResponse> VerifyTokenService(VerifyToken request)
        {
            var token = request.token;
            var email = request.email;
            var tokenExist = await _distributedCache.GetStringAsync($"TokenForgetPassword:{email}");
            if (tokenExist != token)
            {
                throw new BadRequestHttpException("Token is invalid");
            }
            return new ApiResponse(200, "Token is valid", null, null);
        }
        public async Task<ApiResponse> ResetPasswordService(ResetPassword request)
        {
            var email = request.email;
            var password = request.password;
            var userExist = await _usersRepository.GetEmailUser(email);
            if (userExist == null)
            {
                throw new BadRequestHttpException("Email not found");
            }
            var hashedPassword = _passwordHasher.HashPassword(password);
            userExist.Password = hashedPassword;
            var userToUpdate = new Customer
            {
                Password = hashedPassword
            };
            await _usersRepository.UpdateAsync(userExist, userToUpdate);
            return new ApiResponse(200, "Reset password successful", null, null);
        }
    }
}
