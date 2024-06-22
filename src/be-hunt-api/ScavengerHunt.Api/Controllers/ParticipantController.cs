using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ScavengerHunt.Api.DTOs.Assignment;
using ScavengerHunt.Api.DTOs.Hunt;
using ScavengerHunt.Domain.Repositories;
using StackExchange.Redis;
using IAuthenticationService = ScavengerHunt.Api.Services.IAuthenticationService;

namespace ScavengerHunt.Api.Controllers
{
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public sealed class ParticipantController(IAuthenticationService authService, IConnectionMultiplexer redis, IParticipationRepository participationRepository, IMapper mapper) : ControllerBase
    {
        private readonly IAuthenticationService _authService = authService;
        private readonly IMapper _mapper = mapper;
        private readonly IConnectionMultiplexer _redis = redis;
        private readonly IParticipationRepository _participationRepository = participationRepository;

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _authService.Authenticate(loginRequest.Username, loginRequest.Password);
            if (token is null || token == "")
            {
                return Unauthorized("Invalid username or password.");
            }
            
            var db = _redis.GetDatabase();
            await db.StringSetAsync(token, loginRequest.Username);

            return Ok(new { Token = token });
        }

        [HttpGet("Assignment")]
        public async Task<ActionResult<AssignmentGetPrivateDto>> GetCurrentTask([FromHeader] string token)
        {
            var db = _redis.GetDatabase();
            var username = await db.StringGetAsync(token);

            if(username.IsNullOrEmpty)
            {
                return Unauthorized("Invalid token.");
            }

            var participation = await _participationRepository.GetLatestByUsernameAsync(username!);

            if(participation is null)
            {
                return NotFound("No participation found.");
            }
            

            return Ok(_mapper.Map<AssignmentGetPrivateDto>(participation.CurrentAssignment));
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
