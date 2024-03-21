using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class ParticipationDtoController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;
        private readonly IMapper _mapper;

        public ParticipationDtoController(ScavEditorApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ParticipationDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipationDto>>> GetParticipationDto()
        {
            var participations = await _context.Participation.ToListAsync();
            var participationsDto = _mapper.Map<List<ParticipationDto>>(participations);
            return participationsDto;
        }

        // GET: api/ParticipationDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipationDto>> GetParticipationDto(int id)
        {
            var participation = await _context.Participation.FindAsync(id);
            var participationDto = _mapper.Map<ParticipationDto>(participation);

            if (participationDto is null)
                return NotFound();

            return participationDto;
        }

        // PUT: api/ParticipationDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipationDto(int id, ParticipationDto participationDto)
        {
            if (id != participationDto.Id)
                return BadRequest();

            var participation = await _context.Participation.FindAsync(id);

            if(participation is null)
                return NotFound();

            _context.Entry(participation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipationExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/ParticipationDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParticipationDto>> PostParticipationDto(ParticipationDto participationDto)
        {
            _context.Participation.Add(_mapper.Map<Participation>(participationDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipationDto", new { id = participationDto.Id }, participationDto);
        }

        // DELETE: api/ParticipationDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipationDto(int id)
        {
            var participation = await _context.Participation.FindAsync(id);
            if (participation is null)
                return NotFound();

            _context.Participation.Remove(participation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipationExists(int id)
        {
            return _context.Participation.Any(e => e.Id == id);
        }
    }
}
