using Microsoft.AspNetCore.Mvc;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Domain.Repositories;

namespace ScavengerHunt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly IStationRepository _stationRepository;

        public StationsController(IStationRepository repository)
        {
            _stationRepository = repository;
        }

        // GET: api/Stations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Station>>> GetStation()
        {
            var stations = await _stationRepository.GetAll();
            return Ok(stations);
        }

        // GET: api/Stations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Station>> GetStation(int id)
        {
            var station = await _stationRepository.GetByIdAsync(id);

            if (station is null)
                return NotFound();

            return Ok(station);
        }

        // PUT: api/Stations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStation(int id, Station station)
        {
            if (id != station.Id)
                return BadRequest();

            var stationold = await _stationRepository.GetByIdAsync(id);

            if (stationold is null)
                return NotFound();

            var res = await _stationRepository.UpdateAsync(station);

            if (res)
                return Ok(res);

            return BadRequest();
        }

        // POST: api/Stations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Station>> PostStation(Station station)
        {
            var id = await _stationRepository.AddAsync(station);

            if (id <= 0)
                return BadRequest();
            return CreatedAtAction(nameof(PostStation), new { id = station.Id }, station.Name);
        }

        // DELETE: api/Stations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            var station = await _stationRepository.DeleteByIdAsync(id);

            if (station is null)
                return NotFound();

            return Ok(station);
        }

        private bool StationExists(int id)
        {
            var station = _stationRepository.GetByIdAsync(id);
            return station is not null;
        }
    }
}
