using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Core.DTOs.Auth.Login;
using Core.DTOs.Auth.Register;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthLoginResponse>> Login(AuthLoginRequest request)

        {
            var response = await _authService.LoginUser(request);

            if (response is null) 
                return NotFound("Wrong email or password!");

            return Created(string.Empty, response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthLoginResponse>> Login(AuthRegistrationRequest request)

        {
            var response = await _authService.RegisterUser(request);

            if (response is null)            
                return BadRequest("This account already exists");
            
            return Created(string.Empty, response);
        }
    }
}
