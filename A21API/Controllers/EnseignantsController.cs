using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A21API.Data;
using A21API.Models;

namespace A21API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnseignantsController : ControllerBase
    {
        private readonly A21APIContext _context;

        public EnseignantsController(A21APIContext context)
        {
            _context = context;
        }

        // GET: api/Enseignants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enseignant>>> GetEnseignants()
        {
          if (_context.Enseignants == null)
          {
              return NotFound();
          }
            return await _context.Enseignants.ToListAsync();
        }

        // GET: api/Enseignants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enseignant>> GetEnseignant(int id)
        {
          if (_context.Enseignants == null)
          {
              return NotFound();
          }
            var enseignant = await _context.Enseignants.FindAsync(id);

            if (enseignant == null)
            {
                return NotFound();
            }

            return enseignant;
        }

        // PUT: api/Enseignants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnseignant(int id, Enseignant enseignant)
        {
            if (id != enseignant.Id)
            {
                return BadRequest();
            }

            _context.Entry(enseignant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnseignantExists(id))
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

        // POST: api/Enseignants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Enseignant>> PostEnseignant(Enseignant enseignant)
        {
          if (_context.Enseignants == null)
          {
              return Problem("Entity set 'A21APIContext.Enseignants'  is null.");
          }
            _context.Enseignants.Add(enseignant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnseignant", new { id = enseignant.Id }, enseignant);
        }

        // DELETE: api/Enseignants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnseignant(int id)
        {
            if (_context.Enseignants == null)
            {
                return NotFound();
            }
            var enseignant = await _context.Enseignants.FindAsync(id);
            if (enseignant == null)
            {
                return NotFound();
            }

            _context.Enseignants.Remove(enseignant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnseignantExists(int id)
        {
            return (_context.Enseignants?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
