using AutoMapper;
using System.Text.Json;
using Hunts.Api.DTOs.Hunt;
using Hunts.Api.Services;
using Hunts.Domain.Entities;
using Hunts.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Hunts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class HuntsController(IHuntRepository hrepository, IMapper mapper, IMessageBusClient messageBus) : ControllerBase
    {
        private readonly IHuntRepository _huntRepository = hrepository;
        private readonly IMapper _mapper = mapper;
        private readonly IMessageBusClient messageBus = messageBus;

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

        [HttpGet("server-ip")]
        public async Task<IActionResult> GetServerIPAddress()
        {
            try
            {   
                string filePath = Path.Combine(AppContext.BaseDirectory, "ipconfig.json");
                using StreamReader r = new StreamReader(filePath);
                string json = await r.ReadToEndAsync();
                var config = JsonSerializer.Deserialize<IpAdress>(json);
                Console.WriteLine(config?.Ip);
                if (config?.Ip == null)
                    return BadRequest(new { error = "Couldn not finde a IP Adress" });

                return Ok(new { ip = config?.Ip });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
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

            var updatedHunt = _mapper.Map<Hunt>(scavengerHuntDto);

            if (updatedHunt is null)
                return BadRequest();

            var res = await _huntRepository.DeleteByIdAsync(id);
            var res2 = await _huntRepository.AddAsync(updatedHunt);

            if (res2 == -1)
                return BadRequest();

            try
            {
                var huntPublish = _mapper.Map<HuntPublishDto>(updatedHunt);
                huntPublish.Event = "Hunt_Updated";
                messageBus.PublishNewHunt(huntPublish);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't publish new hunt to message bus: {ex.Message}");
            }

            return Ok(true);
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
            try
            {
                var huntPublish = _mapper.Map<HuntPublishDto>(scavengerHunt);
                huntPublish.Event = "Hunt_Created";
                messageBus.PublishNewHunt(huntPublish);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't publish new hunt to message bus: {ex.Message}");
            }

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

            try
            {
                var huntPublish = _mapper.Map<HuntPublishDto>(hunt);
                huntPublish.Event = "Hunt_Deleted";
                messageBus.PublishNewHunt(huntPublish);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't publish new hunt to message bus: {ex.Message}");
            }

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
