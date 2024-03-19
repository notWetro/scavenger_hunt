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
    public class ScavengerHuntDtoesController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;

        public ScavengerHuntDtoesController(ScavEditorApiContext context)
        {
            _context = context;
        }

        // GET: api/ScavengerHuntDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScavengerHuntDto>>> GetScavengerHuntDto()
        {
            return await _context.ScavengerHuntDto.ToListAsync();
        }

        // GET: api/ScavengerHuntDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScavengerHuntDto>> GetScavengerHuntDto(int id)
        {
            var scavengerHuntDto = await _context.ScavengerHuntDto.FindAsync(id);

            if (scavengerHuntDto == null)
            {
                return NotFound();
            }

            return scavengerHuntDto;
        }

        // PUT: api/ScavengerHuntDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScavengerHuntDto(int id, ScavengerHuntDto scavengerHuntDto)
        {
            if (id != scavengerHuntDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(scavengerHuntDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScavengerHuntDtoExists(id))
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

        // POST: api/ScavengerHuntDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScavengerHuntDto>> PostScavengerHuntDto(ScavengerHuntDto scavengerHuntDto)
        {
            _context.ScavengerHuntDto.Add(scavengerHuntDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScavengerHuntDto", new { id = scavengerHuntDto.Id }, scavengerHuntDto);
        }

        // DELETE: api/ScavengerHuntDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScavengerHuntDto(int id)
        {
            var scavengerHuntDto = await _context.ScavengerHuntDto.FindAsync(id);
            if (scavengerHuntDto == null)
            {
                return NotFound();
            }

            _context.ScavengerHuntDto.Remove(scavengerHuntDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScavengerHuntDtoExists(int id)
        {
            return _context.ScavengerHuntDto.Any(e => e.Id == id);
        }
    }
}
