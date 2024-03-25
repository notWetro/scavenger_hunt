using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScavEditor.Api.Data;
using ScavEditor.Api.Models;

namespace ScavEditor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScavengerHuntsController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;

        public ScavengerHuntsController(ScavEditorApiContext context)
        {
            _context = context;
        }

        // GET: api/ScavengerHunts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScavengerHunt>>> GetScavengerHunt()
        {
            return await _context.ScavengerHunt.ToListAsync();
        }

        // GET: api/ScavengerHunts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScavengerHunt>> GetScavengerHunt(int id)
        {
            var scavengerHunt = await _context.ScavengerHunt.FindAsync(id);

            if (scavengerHunt == null)
            {
                return NotFound();
            }

            return scavengerHunt;
        }

        // PUT: api/ScavengerHunts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScavengerHunt(int id, ScavengerHunt scavengerHunt)
        {
            if (id != scavengerHunt.Id)
            {
                return BadRequest();
            }

            _context.Entry(scavengerHunt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScavengerHuntExists(id))
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

        // POST: api/ScavengerHunts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScavengerHunt>> PostScavengerHunt(ScavengerHunt scavengerHunt)
        {
            _context.ScavengerHunt.Add(scavengerHunt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScavengerHunt", new { id = scavengerHunt.Id }, scavengerHunt);
        }

        // DELETE: api/ScavengerHunts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScavengerHunt(int id)
        {
            var scavengerHunt = await _context.ScavengerHunt.FindAsync(id);
            if (scavengerHunt == null)
            {
                return NotFound();
            }

            _context.ScavengerHunt.Remove(scavengerHunt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScavengerHuntExists(int id)
        {
            return _context.ScavengerHunt.Any(e => e.Id == id);
        }
    }
}
