using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Participants.Api.DTOs.Login;
using Participants.Api.DTOs.Participation;
using Participants.Api.Services;
using Participants.Domain.Entities;
using Participants.Domain.Repositories;

namespace Participants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ParticipationsController(IParticipantRepository prepository, IParticipationRepository parepository, ICache cache, IMapper mapper) : ControllerBase
    {
        private readonly IParticipantRepository _participantRepository = prepository;
        private readonly IParticipationRepository _participationRepository = parepository;
        private readonly ICache _cache = cache;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// POST Hunt/{huntId}: Creates a participation.
        /// </summary>
        /// <param name="huntId">Identifier of a hunt</param>
        /// <param name="credentials">Credentials of the user (e.g. username and password)</param>
        /// <returns>Participation information when success.</returns>
        [HttpPost("Hunt/{huntId}")]
        public async Task<ActionResult<ParticipationGetDto>> PostParticipation(int huntId, [FromBody] UserCredentials credentials)
        {
            var existingParticipation = await _participationRepository.GetByIdAndUsernameAsync(huntId, credentials.Username);
            if (existingParticipation is not null)
                return Conflict("Participation already exists for the given hunt and username.");

            var hunt = await _cache.GetHuntAsync(huntId);
            if (hunt is null)
                return NotFound("Specified hunt-id has no associated hunt-object.");

            // The participant will be created if it doesn't exist right now
            var participant = await _participantRepository.GetByUsernameAsync(credentials.Username);

            if (participant is null)
            {
                participant = new Participant() { Username = credentials.Username, Password = credentials.Password, Participations = [] };
                await _participantRepository.AddAsync(participant);
            }

            var participation = new Participation()
            {
                Participant = participant,
                HuntId = huntId,
                CurrentAssignmentId = hunt.Assignments.First().Id,
                Status = ParticipationStatus.Running,
            };

            await _participationRepository.AddAsync(participation);
            return Ok(_mapper.Map<ParticipationGetDto>(participation));
        }

        [HttpGet("Stats")]
        public async Task<ActionResult<ParticipationStatsDto>> GetParticipationStats()
        {
            var participants = await _participantRepository.GetAll();
            var paricipations = await _participationRepository.GetAll();

            var participationStats = new ParticipationStatsDto()
            {
                ParticipantCount = participants.Count(),
                ParticipationCount = paricipations.Count()
            };

            return Ok(participationStats);
        }
    }
}
