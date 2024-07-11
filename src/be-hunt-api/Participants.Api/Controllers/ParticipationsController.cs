using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Participants.Api.DTOs.Participation;
using Participants.Api.Services;
using Participants.Domain.Entities;
using Participants.Domain.Repositories;

namespace Participants.Api.Controllers
{
    public sealed class UserCredentials
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public sealed class ParticipationsController(IParticipantRepository prepository, IParticipationRepository parepository, ICache cache, IMapper mapper) : ControllerBase
    {
        private readonly IParticipantRepository _participantRepository = prepository;
        private readonly IParticipationRepository _participationRepository = parepository;
        private readonly ICache _cache = cache;
        private readonly IMapper _mapper = mapper;

        [HttpPost("Hunt/{huntId}")]
        public async Task<ActionResult<ParticipationGetDto>> PostParticipation(int huntId, [FromBody] UserCredentials cred)
        {
            var existingParticipation = await _participationRepository.GetByIdAndUsernameAsync(huntId, cred.Username);
            if (existingParticipation is not null)
                return Conflict("Participation already exists for the given hunt and username.");

            var hunt = await _cache.GetHuntAsync(huntId);
            if(hunt is null)
                return NotFound("Specified hunt-id has no associated hunt-object.");

            var participant = await _participantRepository.GetByUsernameAsync(cred.Username);

            if (participant is null)
            {
                participant = new Participant() { Username = cred.Username, Password = cred.Password, Participations = [] };
                await _participantRepository.AddAsync(participant);
            }

            var participation = new Participation()
            {
                Participant = participant,
                HuntId = huntId,
                CurrentAssignmentId = hunt.Assignments.First()
            };

            participant.Participations.Add(participation);

            await _participationRepository.AddAsync(participation);

            return Ok(_mapper.Map<ParticipationGetDto>(participation));
        }
    }
}
