using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Users;
using Restapi_net8.Services.Interface;

namespace Restapi_net8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService userService;
        public UsersController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            this.userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUsers request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userCreate = await userService.CreateUserService(request);
            return Ok(userCreate);

        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUser request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userLogin = await userService.LoginUserService(request);
            return Ok(userLogin);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshToken request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var refreshToken = await userService.RefreshTokenService(request);
            return Ok(refreshToken);
        }
        [Authorize]
        [HttpGet("user-info")]
        public async Task<IActionResult> UserInfo()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userInfo = await userService.UserInfoService(userId);
            return Ok(userInfo);
        }
        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var logout = await userService.LogoutService(userId);
            return Ok(logout);
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUsers request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userUpdate = await userService.UpdateUserService(request, userId);
            return Ok(userUpdate);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var forgotPassword = await userService.ForgotPasswordService(request);
            return Ok(forgotPassword);
        }
        [HttpPost("verify-token")]
        public async Task<IActionResult> VerifyToken([FromBody] VerifyToken request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var verifyToken = await userService.VerifyTokenService(request);
            return Ok(verifyToken);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resetPassword = await userService.ResetPasswordService(request);
            return Ok(resetPassword);
        }
        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var changePassword = await userService.ChangePasswordService(request, userId);
            return Ok(changePassword);
        }
        [Authorize]
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var verifyEmail = await userService.VerifyEmailService(userEmail);
            return Ok(verifyEmail);
        }
    }
}
