using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScavengerHunt.Api.DTOs.Hunt;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Domain.Repositories;

namespace ScavEditor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class HuntsController(IHuntRepository hrepository, IMapper mapper) : ControllerBase
    {
        private readonly IHuntRepository _huntRepository = hrepository;
        private readonly IMapper _mapper = mapper;

        #region HTTP GETs

        /// <summary>
        /// Format: GET: api/Hunts
        /// Get all scavenger hunts.
        /// </summary>
        /// <returns>List of scavenger hunts.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HuntGetDto>>> GetScavengerHunt()
        {
            var hunts = await _huntRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<HuntGetDto>>(hunts));
        }

        /// <summary>
        /// Format: GET: api/Hunts/5
        /// Get a scavenger hunt
        /// </summary>
        /// <param name="id">Id of desired scavenger hunt.</param>
        /// <returns>Requested scavenger hunt if it exists.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Hunt>> GetScavengerHunt(int id)
        {
            var scavengerHunt = await _huntRepository.GetByIdAsync(id);

            if (scavengerHunt is null)
                return NotFound();

            return Ok(_mapper.Map<HuntGetDto>(scavengerHunt));
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
        public async Task<IActionResult> PutScavengerHunt(int id, HuntUpdateDto scavengerHuntDto)
        {
            if (id != scavengerHuntDto.Id)
                return BadRequest();

            var hunt = await _huntRepository.GetByIdAsync(id);

            if (hunt is null)
                return NotFound();

            // TODO: This is scary. Maybe change it a bit.
            hunt.Title = scavengerHuntDto.Title;
            hunt.Description = scavengerHuntDto.Description;

            var res = await _huntRepository.UpdateAsync(hunt);

            if (res)
                return Ok(res);

            return BadRequest();
        }

        #endregion

        #region HTTP POSTs

        /// <summary>
        /// Format: POST: api/Hunts
        /// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// </summary>
        /// <param name="scavengerHuntDto">New scavenger hunt object to be added.</param>
        /// <returns>Ok-Code 201 and title of scavenger hunt on success.</returns>
        [HttpPost]
        public async Task<ActionResult<HuntGetDto>> PostScavengerHunt(HuntCreateDto scavengerHuntDto)
        {
            var scavengerHunt = _mapper.Map<Hunt>(scavengerHuntDto);

            var id = await _huntRepository.AddAsync(scavengerHunt);

            if (id <= 0)
                return BadRequest();
            
            return CreatedAtAction(nameof(PostScavengerHunt), _mapper.Map<HuntGetDto>(scavengerHunt));
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

            return Ok(_mapper.Map<HuntGetDto>(hunt));
        }

        #endregion

        private bool ScavengerHuntExists(int id)
        {
            var hunt = _huntRepository.GetByIdAsync(id);
            return hunt is not null;
        }
    }
}
