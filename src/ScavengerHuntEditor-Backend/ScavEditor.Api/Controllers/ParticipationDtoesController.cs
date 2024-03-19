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
    public class ParticipationDtoesController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;

        public ParticipationDtoesController(ScavEditorApiContext context)
        {
            _context = context;
        }

        // GET: api/ParticipationDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipationDto>>> GetParticipationDto()
        {
            return await _context.ParticipationDto.ToListAsync();
        }

        // GET: api/ParticipationDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipationDto>> GetParticipationDto(int id)
        {
            var participationDto = await _context.ParticipationDto.FindAsync(id);

            if (participationDto == null)
            {
                return NotFound();
            }

            return participationDto;
        }

        // PUT: api/ParticipationDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipationDto(int id, ParticipationDto participationDto)
        {
            if (id != participationDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(participationDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipationDtoExists(id))
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

        // POST: api/ParticipationDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParticipationDto>> PostParticipationDto(ParticipationDto participationDto)
        {
            _context.ParticipationDto.Add(participationDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipationDto", new { id = participationDto.Id }, participationDto);
        }

        // DELETE: api/ParticipationDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipationDto(int id)
        {
            var participationDto = await _context.ParticipationDto.FindAsync(id);
            if (participationDto == null)
            {
                return NotFound();
            }

            _context.ParticipationDto.Remove(participationDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipationDtoExists(int id)
        {
            return _context.ParticipationDto.Any(e => e.Id == id);
        }
    }
}
