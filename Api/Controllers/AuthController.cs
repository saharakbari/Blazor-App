using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {

            try
            {
                var token =await _loginUserService.LoginAsync(request.Email, request.PasswordHash);
                // return new JsonResult(token);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {

            try
            {
                await _registerUserService.RegisterAsync(request.Email, request.UserName, request.PasswordHash
                    ,(Domain.Entities.UserRole)request.Role);
                // return new JsonResult(token);
                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



    
        public record RegisterVM(string email, string username, string password, UserRole role);

       
    }
}
