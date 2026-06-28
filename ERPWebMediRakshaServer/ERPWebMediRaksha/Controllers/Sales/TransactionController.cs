using ERP.EntitiesCore.Sales;
using ERP.ServicesCore.Interfaces.Sales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPWebMediRaksha.Controllers.Sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions(int pageNumber = 1, int pageSize = 15)
        {
            var result = await _service.GetTransactions(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet("details/{saleId}")]
        public async Task<IActionResult> GetSaleDetails(int saleId)
        {
            var result = await _service.GetSaleDetails(saleId);
            return Ok(result);
        }
        [HttpPost("filter")]
        public async Task<IActionResult> FilterTransactions(TransactionFilterRequest request)
        {
            var result = await _service.FilterTransactions(request);
            return Ok(result);
        }
    }
}
