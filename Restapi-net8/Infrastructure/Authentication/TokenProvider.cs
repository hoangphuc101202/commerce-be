using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Restapi_net8.Infrastructure.Authentication
{
    public sealed class TokenProvider
    {
        private readonly IConfiguration _configuration;
        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        internal (string accessToken, string refreshToken) GenerateJsonWebToken(PayloadUsers user){ 
            string privateKey = _configuration["AppSettings:PrivateKey"];
            string publicKey = _configuration["AppSettings:PublicKey"];
            var secretKeyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));
            var publicketBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(publicKey));
            var credentialsPublic = new SigningCredentials(publicketBytes, SecurityAlgorithms.HmacSha256);
            var credentials = new SigningCredentials(secretKeyBytes, SecurityAlgorithms.HmacSha256);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.IdUser),
                    new Claim("FullName", user.FullName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("AppSettings:TokenExpiry")),
                SigningCredentials = credentials,
            };
            var TokenDescriptorRefresh = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.IdUser),
                    new Claim("FullName", user.FullName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("AppSettings:TokenExpiryInDays")),
                SigningCredentials = credentialsPublic,
            };
            var handler = new JsonWebTokenHandler();
            string accessToken = handler.CreateToken(TokenDescriptor);
            string refreshToken = handler.CreateToken(TokenDescriptorRefresh);
            return (accessToken, refreshToken);
        }
      
    }
}
