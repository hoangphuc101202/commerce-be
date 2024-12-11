using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Restapi_net8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }
        [Authorize]
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var invoiceCreated = await invoiceService.Checkout(request, userId);
            return Ok(invoiceCreated);
        }
        [Authorize]
        [HttpPost("order")]
        public async Task<IActionResult> Order([FromBody] OrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var invoiceCreated = await invoiceService.Order(request, userId);
            return Ok(invoiceCreated);
        }
        [Authorize]
        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var invoiceStatus = await invoiceService.GetStatus();
            return Ok(invoiceStatus);
        }
        [Authorize]
        [HttpGet("shipping-status")]
        public async Task<IActionResult> GetShippingStatus()
        {
            var shippingStatus = await invoiceService.GetShippingStatus();
            return Ok(shippingStatus);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllInvoiceRequest request)
        {
            var invoices = await invoiceService.GetAll(request);
            return Ok(invoices);
        }
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetOrderOfUser()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var invoices = await invoiceService.getOrderOfUser(Guid.Parse(userId));
            return Ok(invoices);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice([FromRoute] string id)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var invoice = await invoiceService.GetInvoice(id, role, userId);
            return Ok(invoice);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("admin/{id}")]
        public async Task<IActionResult> UpdateInvoice([FromRoute] string id, [FromBody] UpdateInvoiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var invoiceUpdated = await invoiceService.UpdateInvoiceForAdmin(request, id);
            return Ok(invoiceUpdated);
        }
    }
}
