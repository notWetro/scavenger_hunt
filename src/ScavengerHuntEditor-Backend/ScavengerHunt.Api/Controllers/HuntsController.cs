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
            throw new NotImplementedException();
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

            throw new NotImplementedException();
            //// RETURN IF NOT EXISTS
            //if (!ScavengerHuntExists(id))
            //    return NotFound();
            //// RETURN IF EXISTS
            //return NoContent();
        }

        // POST: api/Hunts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hunt>> PostScavengerHunt(Hunt scavengerHunt)
        {
            await _huntRepository.Add(scavengerHunt);
            return CreatedAtAction("GetHunt", new { id = scavengerHunt.Id }, scavengerHunt);
        }

        // DELETE: api/Hunts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScavengerHunt(int id)
        {
            throw new NotImplementedException();
            //var scavengerHunt = await _context.ScavengerHunt.FindAsync(id);
            //if (scavengerHunt == null)
            //    return NotFound();
            //_context.ScavengerHunt.Remove(scavengerHunt);
            //await _context.SaveChangesAsync();
            //return NoContent();
        }

        private bool ScavengerHuntExists(int id)
        {
            throw new NotImplementedException();
            //return _context.ScavengerHunt.Any(e => e.Id == id);
        }
    }
}
