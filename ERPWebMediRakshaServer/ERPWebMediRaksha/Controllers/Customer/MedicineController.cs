using ERP.ServicesCore.Interfaces.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPWebMediRaksha.Controllers.Customer
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _service;

        public MedicineController(IMedicineService service)
        {
            _service = service;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string searchText)
        {
            var result = await _service.SearchProducts(searchText);
            return Ok(result);
        }
    }
}
