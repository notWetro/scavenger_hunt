using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScavEditor.Api.Data;
using ScavEditor.Api.Models;

namespace ScavEditor.Api.Controllers
{
    /// <summary>
    /// ParticipantsController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantsController"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public ParticipantsController(ScavEditorApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of participants.
        /// </summary>
        /// <returns>List of Participants.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipant()
        {
            return await _context.Participant.ToListAsync();
        }

        /// <summary>
        /// Gets a participant by ID.
        /// </summary>
        /// <param name="id">Participant Id.</param>
        /// <returns>Participant.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipant(int id)
        {
            var participant = await _context.Participant.FindAsync(id);

            if (participant == null)
            {
                return NotFound();
            }

            return participant;
        }

        /// <summary>
        /// Updates a participant.
        /// </summary>
        /// <param name="id">Id of Participant.</param>
        /// <param name="participant">Participant Update Details.</param>
        /// <returns>Updated Participant.</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant(int id, Participant participant)
        {
            if (id != participant.Id)
            {
                return BadRequest();
            }

            _context.Entry(participant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
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
        /// Creates a new participant.
        /// </summary>
        /// <param name="participant"></param>
        /// <returns>Participant.</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participant>> PostParticipant(Participant participant)
        {
            _context.Participant.Add(participant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipant", new { id = participant.Id }, participant);
        }

        /// <summary>
        /// Deletes a participant.
        /// </summary>
        /// <param name="id">Id of Participant to delete.</param>
        /// <returns>NoContentResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            var participant = await _context.Participant.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            _context.Participant.Remove(participant);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        /// <summary>
        /// Checks if a participant exists.
        /// </summary>
        /// <param name="id">Id of participant to check.</param>
        /// <returns>True if participant exists.</returns>
        private bool ParticipantExists(int id)
        {
            return _context.Participant.Any(e => e.Id == id);
        }
    }
}
