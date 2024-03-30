using Microsoft.AspNetCore.Mvc;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Domain.Repositories;

namespace ScavengerHunt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController(IStationRepository repository) : ControllerBase
    {
        private readonly IStationRepository _stationRepository = repository;

        #region HTTP GETs

        /// <summary>
        /// Format: GET api/Stations
        /// Get all stations.
        /// </summary>
        /// <returns>List of stations.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Station>>> GetStation()
        {
            var stations = await _stationRepository.GetAll();
            return Ok(stations);
        }

        /// <summary>
        /// Format: GET: api/Stations/5
        /// Get a station
        /// </summary>
        /// <param name="id">Id of desired station.</param>
        /// <returns>Requested station if it exists.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Station>> GetStation(int id)
        {
            var station = await _stationRepository.GetByIdAsync(id);

            if (station is null)
                return NotFound();

            return Ok(station);
        }

        #endregion

        #region HTTP PUTs

        /// <summary>
        /// PUT: api/Stations/5
        /// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// </summary>
        /// <param name="id"></param>
        /// <param name="station"></param>
        /// <returns>Ok-Code 200 if request was fulfilled.</returns>
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

        #endregion

        #region HTTP POSTs

        /// <summary>
        /// Format: POST: api/Stations
        /// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// </summary>
        /// <param name="station">New station object to be added.</param>
        /// <returns>Ok-Code 201 and title of station on success.</returns>
        [HttpPost]
        public async Task<ActionResult<Station>> PostStation(Station station)
        {
            var id = await _stationRepository.AddAsync(station);

            if (id <= 0)
                return BadRequest();
            return CreatedAtAction(nameof(PostStation), new { id = station.Id, name = station.Name });
        }

        #endregion

        #region HTTP DELETEs

        /// <summary>
        /// Format: DELETE: api/Stations/5
        /// </summary>
        /// <param name="id">Id of desired station to be deleted.</param>
        /// <returns>The station object if request was fulfilled.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            var station = await _stationRepository.DeleteByIdAsync(id);

            if (station is null)
                return NotFound();

            return Ok(station);
        }

        #endregion

        private bool StationExists(int id)
        {
            var station = _stationRepository.GetByIdAsync(id);
            return station is not null;
        }
    }
}
