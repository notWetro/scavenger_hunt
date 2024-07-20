using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Participants.Api.DTOs.Login;
using Participants.Api.Services;
using Participants.Domain;
using Participants.Domain.Entities;
using Participants.Domain.Repositories;
using IAuthenticationService = Participants.Api.Services.IAuthenticationService;

namespace Participants.Api.Controllers
{
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class GetCurrentAssignmentResponse
    {
        public int HintType { get; set; }
        public required string HintData { get; set; }
        public int SolutionType { get; set; }
    }

    public class PostSubmitSolutionRequest
    {
        public required string Data { get; set; }
    }

    public class PostSubmitSolutionResponse
    {
        public bool Success { get; set; }
        public required string HintData { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public sealed class ParticipantsController(IAuthenticationService authService, ICache cache, IParticipationRepository participationRepository) : ControllerBase
    {
        private readonly IAuthenticationService _authService = authService;
        private readonly ICache _cache = cache;
        private readonly IParticipationRepository _participationRepository = participationRepository;

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _authService.Authenticate(loginRequest.Username, loginRequest.Password);
            if (token is null || token == "")
            {
                return Unauthorized("Invalid username or password.");
            }

            await _cache.SaveLoginToken(loginRequest.Username, token);

            var participations = await _participationRepository.GetByUsernameAsync(loginRequest.Username);

            var hunts = await Task.WhenAll(participations.Select(async participation =>
            {
                var hunt = await _cache.GetHuntAsync(participation.HuntId);
                return hunt != null ? new HuntLoginDto { Id = participation.HuntId, Title = hunt.Title } : null;
            }));

            var validHunts = hunts.Where(hunt => hunt is not null).Select(hunt => hunt!).ToList();

            var response = new LoginResponseDto()
            {
                Token = token,
                Hunts = validHunts
            };

            return Ok(response);
        }

        [HttpGet("CurrentAssignment/{huntId}")]
        public async Task<ActionResult<GetCurrentAssignmentResponse>> GetCurrentAssignment([FromHeader(Name = "Authorization")] string token, int huntId)
        {
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Authorization-Token is missing!");

            var username = await _cache.GetUsernameByToken(token);

            if (username is null)
                return Unauthorized("Invalid token!");

            var participation = await _participationRepository.GetByIdAndUsernameAsync(huntId, username);

            if (participation is null)
                return NotFound("Couldn't find a participation for given hunt.");

            switch(participation.Status)
            {
                case ParticipationStatus.Invalid:
                    return Forbid("Participation-Status is not valid.");
                case ParticipationStatus.Stopped:
                case ParticipationStatus.Deleted:
                    return Forbid("Scavenger Hunt has been stopped.");
                case ParticipationStatus.Finished:
                    return Redirect("Scavenger Hunt was already completed!");
            }

            if (participation.Status != ParticipationStatus.Running)
                throw new InvalidOperationException("Unexpected participation status.");

            var hunt = await _cache.GetHuntAsync(participation.HuntId);

            if (hunt is null)
                return NotFound("Couldn't find a hunt for a participation.");

            // Take the current assignment and return it to the player
            var assignment = hunt.Assignments.ToList().Find(assignment => assignment.Id == participation.CurrentAssignmentId);

            if (assignment is null)
            {
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

        [HttpPost("SubmitSolution/{huntId}")]
        public async Task<ActionResult<PostSubmitSolutionResponse>> SubmitAssignmentSolution([FromHeader(Name = "Authorization")] string token, int huntId, [FromBody] PostSubmitSolutionRequest data)
        {
            var username = await AuthenticationHelper.GetUsernameIfValidAsync(_cache, token);

            if (username is null)
                return Unauthorized("Invalid token!");

            var participation = await _participationRepository.GetByIdAndUsernameAsync(huntId, username);

            if (participation is null)
                return NotFound("Couldn't find a participation for given hunt.");

            switch (participation.Status)
            {
                case ParticipationStatus.Invalid:
                    return Forbid("Participation-Status is not valid.");
                case ParticipationStatus.Stopped:
                case ParticipationStatus.Deleted:
                    return Forbid("Scavenger Hunt has been stopped.");
                case ParticipationStatus.Finished:
                    return Redirect("Scavenger Hunt was already completed!");
            }

            if (participation.Status != ParticipationStatus.Running)
                throw new InvalidOperationException("Unexpected participation status.");

            var hunt = await _cache.GetHuntAsync(participation.HuntId);

            if (hunt is null)
                return NotFound("Couldn't find a hunt for a participation.");

            var currentAssignment = hunt.Assignments.Where(assignment => assignment.Id == participation.CurrentAssignmentId).FirstOrDefault();

            if (currentAssignment is null)
                return NotFound("Couldn't find a matching current assignment for the given hunt.");

            var isSolutionEqual = AssignmentHelper.CheckAssignmentData(currentAssignment, data.Data);

            if (isSolutionEqual)
            {
                var currentAssignmentIndex = hunt.Assignments.ToList().FindIndex(assignment => assignment.Id == currentAssignment.Id);
                if (currentAssignmentIndex < 0 || currentAssignmentIndex >= hunt.Assignments.Count - 1)
                {
                    // No more assignments left, mark the hunt as finished
                    participation.Status = ParticipationStatus.Finished;
                }
                else
                {
                    // Move to the next assignment
                    var nextAssignment = hunt.Assignments.ToList()[currentAssignmentIndex + 1];
                    participation.CurrentAssignmentId = nextAssignment.Id;
                }

                await _participationRepository.UpdateAsync(participation);

                var participationNew = await _participationRepository.GetByIdAndUsernameAsync(huntId, username);
            }

            return Ok(hunt);
        }
    }
}
