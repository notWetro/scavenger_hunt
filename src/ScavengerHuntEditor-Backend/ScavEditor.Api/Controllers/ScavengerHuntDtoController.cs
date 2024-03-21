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
    public class ScavengerHuntDtoController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;
        private readonly IMapper _mapper;

        public ScavengerHuntDtoController(ScavEditorApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ScavengerHuntDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScavengerHuntDto>>> GetScavengerHuntDto()
        {
            var scavengerHunt = await _context.ScavengerHunt.ToListAsync();
            var scavengerHuntDtos = _mapper.Map<List<ScavengerHuntDto>>(scavengerHunt);
            return scavengerHuntDtos;
        }

        // GET: api/ScavengerHuntDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScavengerHuntDto>> GetScavengerHuntDto(int id)
        {
            var scavengerHunt = await _context.ScavengerHunt.FindAsync(id);
            var scavengerHuntDto = _mapper.Map<ScavengerHuntDto>(scavengerHunt);

            if (scavengerHuntDto is null)
                return NotFound();

            return scavengerHuntDto;
        }

        // PUT: api/ScavengerHuntDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScavengerHuntDto(int id, ScavengerHuntDto scavengerHuntDto)
        {
            if (id != scavengerHuntDto.Id)
                return BadRequest();

            var scavengerHunt = await _context.ScavengerHunt.FindAsync(id);

            if(scavengerHunt is null)
                return NotFound();

            _context.Entry(scavengerHunt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScavengerHuntExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/ScavengerHuntDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScavengerHuntDto>> PostScavengerHuntDto(ScavengerHuntDto scavengerHuntDto)
        {
            _context.ScavengerHunt.Add(_mapper.Map<ScavengerHunt>(scavengerHuntDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScavengerHuntDto", new { id = scavengerHuntDto.Id }, scavengerHuntDto);
        }

        // DELETE: api/ScavengerHuntDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScavengerHuntDto(int id)
        {
            var scavengerHunt = await _context.ScavengerHunt.FindAsync(id);
            if (scavengerHunt is null)
                return NotFound();

            _context.ScavengerHunt.Remove(scavengerHunt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScavengerHuntExists(int id)
        {
            return _context.ScavengerHunt.Any(e => e.Id == id);
        }
    }
}
