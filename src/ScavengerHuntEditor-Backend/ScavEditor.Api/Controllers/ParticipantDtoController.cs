using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScavEditor.Api.DTOs;
using ScavEditor.Api.Data;
using AutoMapper;
using ScavEditor.Api.Models;

namespace ScavEditor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantDtoController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;
        private readonly IMapper _mapper;

        public ParticipantDtoController(ScavEditorApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ParticipantDto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipantDto>>> GetParticipantDto()
        {
            var participants = await _context.Participant.ToListAsync();
            var participantDtos = _mapper.Map<List<ParticipantDto>>(participants);
            return participantDtos;
        }

        // GET: api/ParticipantDto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipantDto>> GetParticipantDto(int id)
        {
            var participant = await _context.Participant.FindAsync(id);
            var participantDto = _mapper.Map<ParticipantDto>(participant);

            if (participantDto is null)
                return NotFound();

            return participantDto;
        }

        // PUT: api/ParticipantDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipantDto(int id, ParticipantDto participantDto)
        {
            if (id != participantDto.Id)
                return BadRequest();

            var participant = await _context.Participant.FindAsync(id);

            if (participant is null)
                return NotFound();

            _context.Entry(participant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/ParticipantDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParticipantDto>> PostParticipantDto(ParticipantDto participantDto)
        {            
            _context.Participant.Add(_mapper.Map<Participant>(participantDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipantDto", new { id = participantDto.Id }, participantDto);
        }

        // DELETE: api/ParticipantDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipantDto(int id)
        {
            var participant = await _context.Participant.FindAsync(id);
            if (participant is null)
                return NotFound();

            _context.Participant.Remove(participant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipantExists(int id)
        {
            return _context.Participant.Any(e => e.Id == id);
        }
    }
}
