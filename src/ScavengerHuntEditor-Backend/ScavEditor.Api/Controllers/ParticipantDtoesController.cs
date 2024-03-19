using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScavEditor.Api.DTOs;
using ScavEditor.Api.Data;

namespace ScavEditor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantDtoesController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;

        public ParticipantDtoesController(ScavEditorApiContext context)
        {
            _context = context;
        }

        // GET: api/ParticipantDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipantDto>>> GetParticipantDto()
        {
            return await _context.ParticipantDto.ToListAsync();
        }

        // GET: api/ParticipantDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipantDto>> GetParticipantDto(int id)
        {
            var participantDto = await _context.ParticipantDto.FindAsync(id);

            if (participantDto == null)
            {
                return NotFound();
            }

            return participantDto;
        }

        // PUT: api/ParticipantDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipantDto(int id, ParticipantDto participantDto)
        {
            if (id != participantDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(participantDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantDtoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ParticipantDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParticipantDto>> PostParticipantDto(ParticipantDto participantDto)
        {
            _context.ParticipantDto.Add(participantDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipantDto", new { id = participantDto.Id }, participantDto);
        }

        // DELETE: api/ParticipantDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipantDto(int id)
        {
            var participantDto = await _context.ParticipantDto.FindAsync(id);
            if (participantDto == null)
            {
                return NotFound();
            }

            _context.ParticipantDto.Remove(participantDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipantDtoExists(int id)
        {
            return _context.ParticipantDto.Any(e => e.Id == id);
        }
    }
}
