using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScavEditor.Api.Data;
using ScavEditor.Api.Models;

namespace ScavEditor.Api.Controllers
{
    /// <summary>
    /// ParticipationsController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipationsController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipationsController"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public ParticipationsController(ScavEditorApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of participations.
        /// </summary>
        /// <returns>list of participations.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participation>>> GetParticipation()
        {
            return await _context.Participation.ToListAsync();
        }

        /// <summary>
        /// Gets a participation by ID.
        /// </summary>
        /// <param name="id">Id of participation.</param>
        /// <returns>Participation.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Participation>> GetParticipation(int id)
        {
            var participation = await _context.Participation.FindAsync(id);

            if (participation == null)
            {
                return NotFound();
            }

            return participation;
        }

        /// <summary>
        /// Updates a participation.
        /// </summary>
        /// <param name="id">Id of participation to update.</param>
        /// <param name="participation">Participation Update details.</param>
        /// <returns>Updated Participation.</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipation(int id, Participation participation)
        {
            if (id != participation.Id)
            {
                return BadRequest();
            }

            _context.Entry(participation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipationExists(id))
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

        /// <summary>
        /// Creates a new participation.
        /// </summary>
        /// <param name="participation">Participation to create.</param>
        /// <returns>Participation.</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participation>> PostParticipation(Participation participation)
        {
            _context.Participation.Add(participation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipation", new { id = participation.Id }, participation);
        }

        /// <summary>
        /// Deletes a participation.
        /// </summary>
        /// <param name="id">Id of participation to delete.</param>
        /// <returns>NoContentResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipation(int id)
        {
            var participation = await _context.Participation.FindAsync(id);
            if (participation == null)
            {
                return NotFound();
            }

            _context.Participation.Remove(participation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        /// <summary>
        /// Checks if a participation exists.
        /// </summary>
        /// <param name="id">Id of participation to check.</param>
        /// <returns>True if participation exists.</returns>
        private bool ParticipationExists(int id)
        {
            return _context.Participation.Any(e => e.Id == id);
        }
    }
}
