using Microsoft.AspNetCore.Mvc;
using Participants.Api.DTOs.CurrentAssignment;
using Participants.Api.DTOs.Login;
using Participants.Api.DTOs.SubmitSolution;
using Participants.Api.Services;
using Participants.Domain;
using Participants.Domain.Entities;
using Participants.Domain.Repositories;
using IAuthenticationService = Participants.Api.Services.IAuthenticationService;

namespace Participants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ParticipantsController(IAuthenticationService authService, ICache cache, IParticipationRepository participationRepository) : ControllerBase
    {
        private readonly IAuthenticationService _authService = authService;
        private readonly ICache _cache = cache;
        private readonly IParticipationRepository _participationRepository = participationRepository;

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
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
                return hunt != null ? new HuntLoginDto { Id = participation.HuntId, Title = hunt.Title, ParticipationStatus = participation.Status } : null;
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
        public async Task<ActionResult<CurrentAssignmentResponseDto>> GetCurrentAssignment([FromHeader(Name = "Authorization")] string token, int huntId)
        {
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Authorization-Token is missing!");

            var username = await _cache.GetUsernameByToken(token);

            if (username is null)
                return Unauthorized("Invalid token!");

            var participation = await _participationRepository.GetByIdAndUsernameAsync(huntId, username);

            if (participation is null)
                return NotFound("Couldn't find a participation for given hunt.");

            switch (participation.Status)
            {
                case ParticipationStatus.Invalid:
                    return NotFound("Participation-Status is not valid.");
                case ParticipationStatus.Stopped:
                case ParticipationStatus.Deleted:
                    return NotFound("Scavenger Hunt has been stopped.");
                case ParticipationStatus.Finished:
                    return new HuntFinishedActionResult(269, "Scavenger Hunt was already completed!");
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

            var response = new CurrentAssignmentResponseDto()
            {
                HintType = assignment.Hint.HintType,
                HintData = assignment.Hint.Data,
                SolutionType = assignment.Solution.SolutionType,
            };

            return Ok(response);
        }

        [HttpPost("SubmitSolution/{huntId}")]
        public async Task<ActionResult<SubmitSolutionResponseDto>> SubmitAssignmentSolution([FromHeader(Name = "Authorization")] string token, int huntId, [FromBody] SubmitSolutionRequestDto data)
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
                    return NotFound("Participation-Status is not valid.");
                case ParticipationStatus.Stopped:
                case ParticipationStatus.Deleted:
                    return NotFound("Scavenger Hunt has been stopped.");
                case ParticipationStatus.Finished:
                    return new HuntFinishedActionResult(269, "Scavenger Hunt was already completed!");
            }

            if (participation.Status != ParticipationStatus.Running)
                throw new InvalidOperationException("Unexpected participation status.");

            var hunt = await _cache.GetHuntAsync(participation.HuntId);

            if (hunt is null)
                return NotFound("Couldn't find a hunt for a participation.");

            var currentAssignment = hunt.Assignments.Where(assignment => assignment.Id == participation.CurrentAssignmentId).FirstOrDefault();

            if (currentAssignment is null)
                return NotFound("Couldn't find a matching current assignment for the given hunt.");

            var isSolutionValid = AssignmentHelper.CheckIfValidSolution(currentAssignment, data.Data);

            if (isSolutionValid)
            {
                await AdvanceToNextAssignmentAsync(_participationRepository, hunt, participation, currentAssignment);

                var submitSolutionResponse = new SubmitSolutionResponseDto()
                {
                    Success = true,
                    HintData = string.Empty
                };

                return Ok(submitSolutionResponse);
            }
            else
            {
                // Give the user some hint to give feedback if he is going to the right direction
                var hintData = GetHintForCurrentAssignment(data.Data, currentAssignment.Solution.Data, currentAssignment.Solution.SolutionType);

                var submitSolutionResponse = new SubmitSolutionResponseDto()
                {
                    Success = false,
                    HintData = hintData
                };

                return Ok(submitSolutionResponse);
            }
        }

        private string GetHintForCurrentAssignment(string userSolutionData, string actualSolutionData, int actualSolutionType) => actualSolutionType switch
        {
            1 => $"{DetermineCharacterDifference(userSolutionData, actualSolutionData)}",
            2 => $"{DetermineLocationDistance(userSolutionData, actualSolutionData)}",
            _ => string.Empty,
        };

        private int DetermineCharacterDifference(string userSolutionData, string actualSolutionData) => LevenshteinUtils.LevenshteinDistance(userSolutionData, actualSolutionData);

        private double DetermineLocationDistance(string userSolutionData, string actualSolutionData)
        {
            // Split the input strings to extract latitude and longitude
            var givenCoords = userSolutionData.Split(';');
            var actualCoords = actualSolutionData.Split(';');

            if (givenCoords.Length != 2 || actualCoords.Length != 2)
            {
                throw new ArgumentException("Input data must be in the format 'lat;lon'");
            }

            // Parse latitude and longitude
            double givenLat = double.Parse(givenCoords[0]);
            double givenLon = double.Parse(givenCoords[1]);
            double actualLat = double.Parse(actualCoords[0]);
            double actualLon = double.Parse(actualCoords[1]);

            // Calculate the distance using the Haversine formula
            double distance = GeoLocatorHelper.Haversine(givenLat, givenLon, actualLat, actualLon);

            // Check if the distance is within the specified area
            return distance;
        }

        private static async Task AdvanceToNextAssignmentAsync(IParticipationRepository participationRepository, Hunt hunt, Participation participation, Assignment currentAssignment)
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

            await participationRepository.UpdateAsync(participation);
        }
    }
}
