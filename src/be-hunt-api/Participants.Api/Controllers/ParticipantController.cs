using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = Participants.Api.Services.IAuthenticationService;

namespace Participants.Api.Controllers
{
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public sealed class ParticipantController(IAuthenticationService authService) : ControllerBase
    {
        private readonly IAuthenticationService _authService = authService;

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _authService.Authenticate(loginRequest.Username, loginRequest.Password);
            if (token is null || token == "")
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { Token = token });
        }

        // [HttpPost("Logout")] probably not needed rn
        //public IActionResult Logout()
        //{
        //    var token = Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        //    if (token == null)
        //    {
        //        return BadRequest("Token is missing.");
        //    }

        //    _authService.Logout(token);
        //    return Ok("Logged out successfully.");
        //}
    }
}
