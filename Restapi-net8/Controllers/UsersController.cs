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

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshToken request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var refreshToken = await userService.RefreshTokenService(request, userId);
            return Ok(refreshToken);
        }
    }
}
