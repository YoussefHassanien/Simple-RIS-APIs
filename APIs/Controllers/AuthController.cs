using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Core.DTOs.Auth.Login;
using Core.DTOs.Auth.Register;
using Azure.Core;

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
            try
            {
                var response = await _authService.LoginUser(request);

                return Created(string.Empty, response);
            }
            catch (NullReferenceException)
            {
                return BadRequest("Wrong email or password!");
            }
            catch (UnauthorizedAccessException)
            {
                return BadRequest("Wrong email or password!");
            }
            catch (Exception) 
            {
                return StatusCode(500);
            }


        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthLoginResponse>> Register(AuthRegistrationRequest request)

        {
            try
            {
                var response = await _authService.RegisterUser(request);

                return Created(string.Empty, response);
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
