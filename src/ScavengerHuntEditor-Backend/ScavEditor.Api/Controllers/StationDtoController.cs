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
    public class StationDtoController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;
        private readonly IMapper _mapper;

        public StationDtoController(ScavEditorApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/StationDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StationDto>>> GetStationDto()
        {
            var stations = await _context.Station.ToListAsync();
            var stationsDto = _mapper.Map<List<StationDto>>(stations);
            return stationsDto;
        }

        // GET: api/StationDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StationDto>> GetStationDto(int id)
        {
            var station = await _context.Station.FindAsync(id);
            var stationDto = _mapper.Map<StationDto>(station);

            if (station is null)
                return NotFound();

            return stationDto;
        }

        // PUT: api/StationDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStationDto(int id, StationDto stationDto)
        {
            if (id != stationDto.Id)
                return BadRequest();

            var station = await _context.Station.FindAsync(id);

            if(station is null)
                return NotFound();

            _context.Entry(station).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/StationDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StationDto>> PostStationDto(StationDto stationDto)
        {
            _context.Station.Add(_mapper.Map<Station>(stationDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStationDto", new { id = stationDto.Id }, stationDto);
        }

        // DELETE: api/StationDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStationDto(int id)
        {
            var stationDto = await _context.Station.FindAsync(id);
            if (stationDto == null)
                return NotFound();

            _context.Station.Remove(stationDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StationExists(int id)
        {
            return _context.Station.Any(e => e.Id == id);
        }
    }
}
