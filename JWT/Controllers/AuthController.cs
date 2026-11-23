using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JWT.Service;
using JWT.Models;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //this controler handles api request
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var result = _authService.Register(request);

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var result = _authService.Login(request);

            return Ok(result);
        }
    }
}
