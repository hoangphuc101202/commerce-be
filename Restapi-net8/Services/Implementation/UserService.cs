using System.IdentityModel.Tokens.Jwt;
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
        public UserService(IUsersRepository usersRepository, PasswordHasher passwordHasher, IMapper mapper, TokenProvider tokenProvider, IDistributedCache distributedCache)    
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _tokenProvider = tokenProvider;
            _distributedCache = distributedCache;
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

        public async Task<ApiResponse> RefreshTokenService(RefreshToken request, string userId)
        {
            var refreshToken = request.refreshToken;
            var refreshTokenExist = await  _distributedCache.GetStringAsync(userId);
            if (!refreshToken.Equals(refreshTokenExist, StringComparison.OrdinalIgnoreCase))
            {
                throw new BadRequestHttpException("Invalid refresh token");
            }
            var payload = DecodeJwtToken(refreshToken);
            var userPayload = new PayloadUsers 
            {
                IdUser = payload.ContainsKey("sub") ? payload["sub"].ToString() : null,
                Email = payload.ContainsKey("email") ? payload["email"].ToString() : null,
                FullName = payload.ContainsKey("FullName") ? payload["FullName"].ToString() : null,
                Role = payload.ContainsKey("Role") ? payload["Role"].ToString() : null
            };
            var (accessToken, newRefreshToken) = _tokenProvider.GenerateJsonWebToken(userPayload);
            var cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
            };
            await _distributedCache.SetStringAsync(userId, newRefreshToken, cacheEntryOptions);
            var token = new {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
            return new ApiResponse(200, "Generate New Refresh Token successful", token, null);

        }
        

    }
}
