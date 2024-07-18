using Microsoft.AspNetCore.Mvc;
using Participants.Api.Services;
using Participants.Domain.Repositories;
using IAuthenticationService = Participants.Api.Services.IAuthenticationService;

namespace Participants.Api.Controllers
{
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class LoginResponse
    {
        public required string Token { get; set; }
        public required string HuntTitle { get; set; }
    }

    public class GetCurrentAssignmentRequest
    {
        public required string Token { get; set; }
    }

    public class GetCurrentAssignmentResponse
    {
        public int HintType { get; set; }
        public required string HintData { get; set; }
        public int SolutionType { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public sealed class ParticipantsController(IAuthenticationService authService, ICache cache, IParticipationRepository participationRepository) : ControllerBase
    {
        private readonly IAuthenticationService _authService = authService;
        private readonly ICache _cache = cache;
        private readonly IParticipationRepository _participationRepository = participationRepository;

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _authService.Authenticate(loginRequest.Username, loginRequest.Password);
            if (token is null || token == "")
            {
                return Unauthorized("Invalid username or password.");
            }

            await _cache.SaveLoginToken(loginRequest.Username, token);


            // TODO: RIGHT NOW ONLY THE FIRST PARTICIPATION IS LOOKED AT!
            var participation = await _participationRepository.GetByUsernameAsync(loginRequest.Username);
            var hunt = await _cache.GetHuntAsync(participation.First().HuntId);

            if (hunt is null)
            {
                return NotFound("Couldn't find a hunt for a participation.");
            }

            var response = new LoginResponse()
            {
                Token = token,
                HuntTitle = hunt.Title
            };

            return Ok(response);
        }

        [HttpPut("CurrentAssignment")]
        public async Task<ActionResult<GetCurrentAssignmentResponse>> GetCurrentAssignment([FromBody] GetCurrentAssignmentRequest currentAssignmentRequest)
        {
            var token = currentAssignmentRequest.Token;

            var username = await _cache.GetUsernameByToken(token);

            if (username is null)
                return Unauthorized("Invalid token");

            // TODO: RIGHT NOW ONLY THE FIRST PARTICIPATION IS LOOKED AT!
            var participation = await _participationRepository.GetByUsernameAsync(username);
            var hunt = await _cache.GetHuntAsync(participation.First().HuntId);

            if (hunt is null)
            {
                return NotFound("Couldn't find a hunt for a participation.");
            }

            // Take the current assignment and return it to the player
            var assignment = hunt.Assignments.ToList().Find(assignment => assignment.Id == participation.First().CurrentAssignmentId);

            if (assignment is null)
            {
                // TODO: Maybe handle this in backend: When no assignment is found, could it be that the hunt is over? Last assignment?
                return NotFound("Couldn't find an assignment");
            }

            var response = new GetCurrentAssignmentResponse()
            {
                HintType = assignment.Hint.HintType,
                HintData = assignment.Hint.Data,
                SolutionType = assignment.Solution.SolutionType,
            };

            return Ok(response);
        }

        [HttpPost("SubmitSolution")]
        public Task<IActionResult> SubmitAssignmentSolution()
        {
            // TODO
            throw new NotImplementedException();
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
