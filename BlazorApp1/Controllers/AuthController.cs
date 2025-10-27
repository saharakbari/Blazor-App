using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUserService _loginUserService;
        private readonly RegisterUserService _registerUserService;
        public AuthController(LoginUserService loginUserService, RegisterUserService registerUserService)
        {
            _loginUserService = loginUserService;
            _registerUserService = registerUserService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM request)
        {

            try
            {
                var token = _loginUserService.LoginAsync(request.Email, request.Password);
                // return new JsonResult(token);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM request)
        {

            try
            {
                await _registerUserService.RegisterAsync(request.email, request.username, request.password
                    , request.role);
                // return new JsonResult(token);
                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



        //This line defines an immutable class named LoginVM with two properties:
        //Email and Password.It is available in C# 9.0 and later.
        public record LoginVM(string Email, string Password);
        public record RegisterVM(string email, string username, string password, UserRole role);
    }
}
