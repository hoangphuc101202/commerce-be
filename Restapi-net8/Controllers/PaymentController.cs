using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restapi_net8.Services.Interface;

namespace Restapi_net8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
         private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paymentCreated  = await paymentService.CreatePayment(request);
            return Ok(paymentCreated);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPaymentRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var payments = await paymentService.GetAll(request);
            return Ok(payments);
        }
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetPaymentByUser()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var payments = await paymentService.GetPaymentByUser(userId);
            return Ok(payments);
        }
    }
}
