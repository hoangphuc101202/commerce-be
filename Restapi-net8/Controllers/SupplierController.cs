using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Restapi_net8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody]CreateSupplierRequestDTO request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var supplierCreated = await supplierService.CreateSupplier(request);
            return Ok(supplierCreated);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById([FromRoute] string id)
        {
            var supplier = await supplierService.GetSupplierById(id);
            return Ok(supplier);
        }
        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier([FromRoute] string id, [FromBody]UpdateSupplierRequestDTO request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var supplierUpdated = await supplierService.UpdateSupplier(id, request);
            return Ok(supplierUpdated);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await supplierService.GetAllSuppliers();
            return Ok(suppliers);
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier([FromRoute] string id)
        {
            var supplierDeleted = await supplierService.DeleteSupplier(id);
            return Ok(supplierDeleted);
        }
    }
}
