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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoice request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var invoiceCreated = await invoiceService.CreateInvoice(request, userId);
            return Ok(invoiceCreated);
        }
    }
}
