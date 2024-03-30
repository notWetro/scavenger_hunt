using Microsoft.AspNetCore.Mvc;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Domain.Repositories;

namespace ScavEditor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class HuntsController(IHuntRepository repository) : ControllerBase
    {
        private readonly IHuntRepository _huntRepository = repository;

        #region HTTP GETs

        /// <summary>
        /// Format: GET: api/Hunts
        /// Get all scavenger hunts.
        /// </summary>
        /// <returns>List of scavenger hunts.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hunt>>> GetScavengerHunt()
        {
            var hunts = await _huntRepository.GetAll();
            return Ok(hunts);
        }

        /// <summary>
        /// Format: GET: api/Hunts/5
        /// </summary>
        /// <param name="id">Id of desired scavenger hunt.</param>
        /// <returns>Requested scavenger hunt if it exists.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Hunt>> GetScavengerHunt(int id)
        {
            var scavengerHunt = await _huntRepository.GetByIdAsync(id);
            
            if (scavengerHunt is null)
                return NotFound();

            return Ok(scavengerHunt);
        }

        #endregion

        #region HTTP PUTs

        /// <summary>
        /// Format: PUT: api/Hunts/5
        /// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// </summary>
        /// <param name="id">Id of scavenger hunt to be edited.</param>
        /// <param name="scavengerHunt">Updated scavenger hunt object.</param>
        /// <returns>Ok-Code 200 if request was fulfilled.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScavengerHunt(int id, Hunt scavengerHunt)
        {
            if (id != scavengerHunt.Id)
                return BadRequest();
            
            var hunt = await _huntRepository.GetByIdAsync(id);

            if(hunt is null)
                return NotFound();

            var res = await _huntRepository.UpdateAsync(scavengerHunt);

            if(res)
                return Ok(res);
            
            return BadRequest();
        }

        #endregion

        #region HTTP POSTs

        /// <summary>
        /// Format: POST: api/Hunts
        /// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// </summary>
        /// <param name="scavengerHunt">New scavenger hunt object to be added.</param>
        /// <returns>Ok-Code 201 and title of scavenger hunt on success.</returns>
        [HttpPost]
        public async Task<ActionResult<Hunt>> PostScavengerHunt(Hunt scavengerHunt)
        {
            var id = await _huntRepository.AddAsync(scavengerHunt);

            if(id <= 0)
                return BadRequest();
            return CreatedAtAction(nameof(PostScavengerHunt), new { id = scavengerHunt.Id }, scavengerHunt.Title);
        }

        #endregion

        #region HTTP DELETEs

        /// <summary>
        /// Format: DELETE: api/Hunts/5
        /// </summary>
        /// <param name="id">Id of desired scavenger hunt to be deleted.</param>
        /// <returns>The scavenger hunt object if request was fulfilled.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScavengerHunt(int id)
        {
            var hunt = await _huntRepository.DeleteByIdAsync(id);
            
            if (hunt is null)
                return NotFound();

            return Ok(hunt);
        }

        #endregion

        private bool ScavengerHuntExists(int id)
        {
            var hunt = _huntRepository.GetByIdAsync(id);
            return hunt is not null;
        }
    }
}
