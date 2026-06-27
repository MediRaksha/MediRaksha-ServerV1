using ERP.ServicesCore.Interfaces.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERP.EntitiesCore.Login;

namespace ERPWebMediRaksha.Controllers.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _service.Login(request);
            return Ok(result);
        }

    }
}
