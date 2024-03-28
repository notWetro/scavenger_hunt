using Microsoft.AspNetCore.Mvc;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Domain.Repositories;
using ScavengerHunt.Infrastructure;

namespace ScavEditor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuntsController : ControllerBase
    {
        private readonly IHuntRepository _huntRepository;

        public HuntsController(IHuntRepository repository)
        {
            _huntRepository = repository;
        }

        // GET: api/Hunts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hunt>>> GetScavengerHunt()
        {
            var hunts = await _huntRepository.GetAll();
            return Ok(hunts);
        }

        // GET: api/Hunts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hunt>> GetScavengerHunt(int id)
        {
            var scavengerHunt = await _huntRepository.GetByIdAsync(id);
            
            if (scavengerHunt is null)
                return NotFound();

            return scavengerHunt;
        }

        // PUT: api/Hunts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Hunts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hunt>> PostScavengerHunt(Hunt scavengerHunt)
        {
            var id = await _huntRepository.AddAsync(scavengerHunt);

            if(id <= 0)
                return BadRequest();
            return CreatedAtAction(nameof(PostScavengerHunt), new { id = scavengerHunt.Id }, scavengerHunt.Title);
        }

        // DELETE: api/Hunts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScavengerHunt(int id)
        {
            var hunt = await _huntRepository.DeleteByIdAsync(id);
            
            if (hunt is null)
                return NotFound();

            return Ok(hunt);
        }

        private bool ScavengerHuntExists(int id)
        {
            var hunt = _huntRepository.GetByIdAsync(id);
            return hunt is not null;
        }
    }
}
