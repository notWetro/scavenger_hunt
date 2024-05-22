using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScavengerHunt.Api.DTOs.Participation;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Domain.Repositories;

namespace ScavengerHunt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ParticipationController(IHuntRepository hrepository, IParticipantRepository prepository, IParticipationRepository parepository, IMapper mapper) : ControllerBase
    {
        private readonly IHuntRepository _huntRepository = hrepository;
        private readonly IParticipantRepository _participantRepository = prepository;
        private readonly IParticipationRepository _participationRepository = parepository;
        private readonly IMapper _mapper = mapper;

        [HttpPost("Hunt/{huntId}")]
        public async Task<ActionResult<ParticipationGetDto>> PostParticipation(int huntId, string username, string password)
        {
            var existingParticipation = await _participationRepository.GetByIdAndUsernameAsync(huntId, username);
            if(existingParticipation is not null)
                return Conflict("Participation already exists for the given hunt and username.");

            var scavengerHunt = await _huntRepository.GetByIdAsync(huntId);

            if (scavengerHunt is null)
                return NotFound("Specified hunt-id has no associated hunt-object.");

            var participant = await _participantRepository.GetByUsernameAsync(username);

            if (participant is null)
            {
                participant = new Participant() { Username = username, Password = "1234" };
                await _participantRepository.AddAsync(participant);
            }

            var participation = new Participation()
            {
                Participant = participant,
                Hunt = scavengerHunt,
                CurrentAssignment = scavengerHunt.Assignments.First()
            };

            await _participationRepository.AddAsync(participation);

            return Ok(_mapper.Map<ParticipationGetDto>(participation));
        }
    }
}
