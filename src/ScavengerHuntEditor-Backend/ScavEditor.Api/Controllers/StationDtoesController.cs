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
    public class StationDtoesController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;

        public StationDtoesController(ScavEditorApiContext context)
        {
            _context = context;
        }

        // GET: api/StationDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StationDto>>> GetStationDto()
        {
            return await _context.StationDto.ToListAsync();
        }

        // GET: api/StationDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StationDto>> GetStationDto(int id)
        {
            var stationDto = await _context.StationDto.FindAsync(id);

            if (stationDto == null)
            {
                return NotFound();
            }

            return stationDto;
        }

        // PUT: api/StationDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStationDto(int id, StationDto stationDto)
        {
            if (id != stationDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(stationDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationDtoExists(id))
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

        // POST: api/StationDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StationDto>> PostStationDto(StationDto stationDto)
        {
            _context.StationDto.Add(stationDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStationDto", new { id = stationDto.Id }, stationDto);
        }

        // DELETE: api/StationDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStationDto(int id)
        {
            var stationDto = await _context.StationDto.FindAsync(id);
            if (stationDto == null)
            {
                return NotFound();
            }

            _context.StationDto.Remove(stationDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StationDtoExists(int id)
        {
            return _context.StationDto.Any(e => e.Id == id);
        }
    }
}
