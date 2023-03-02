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
    public class CrenoHorairesController : ControllerBase
    {
        private readonly A21APIContext _context;

        public CrenoHorairesController(A21APIContext context)
        {
            _context = context;
        }

        // GET: api/CrenoHoraires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrenoHoraire>>> GetAllCrenoHoraires()
        {
          if (_context.CrenoHoraires == null)
          {
              return NotFound();
          }
            return await _context.CrenoHoraires.ToListAsync();
        }


        // GET: api/CrenoHoraires/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CrenoHoraire>> GetCrenoHoraire(int id)
        {
          if (_context.CrenoHoraires == null)
          {
              return NotFound();
          }
            var crenoHoraire = await _context.CrenoHoraires.FindAsync(id);

            if (crenoHoraire == null)
            {
                return NotFound();
            }

            return crenoHoraire;
        }

        // GET: api/CrenoHoraires/EmploiTemps/5
        [HttpGet("EmploiTemps/{emploiTempsId}")]
        public async Task<ActionResult<IEnumerable<CrenoHoraire>>> GetCrenoHoraires(int emploiTempsId)
        {
            if (_context.CrenoHoraires == null)
            {
                return NotFound();
            }
            var crenoHoraires = await _context.CrenoHoraires.Where(c => c.EmploiTempsID == emploiTempsId).ToListAsync();

            if (crenoHoraires == null || crenoHoraires.Count == 0)
            {
                return NotFound();
            }

            return crenoHoraires;
        }

        // PUT: api/CrenoHoraires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrenoHoraire(int id, CrenoHoraire crenoHoraire)
        {
            if (id != crenoHoraire.ID)
            {
                return BadRequest();
            }

            _context.Entry(crenoHoraire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrenoHoraireExists(id))
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

        // PUT: api/CrenoHoraires/Enseignant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Enseignant/{EnseignantId}/{CrenoHorairId}")]
        public async Task<IActionResult> PutCrenoHoraireEnseignant(int EnseignantId,int CrenoHorairId)
        {
            var enseignant = _context.Enseignants.Find(EnseignantId);

            var crenoHoraire = _context.CrenoHoraires.Find(CrenoHorairId);

            if ((enseignant == null)||(crenoHoraire == null))
            {
                return BadRequest();
            }

            crenoHoraire.Enseignant= enseignant;
            crenoHoraire.EnseignantID = EnseignantId;

            _context.Entry(crenoHoraire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrenoHoraireExists(CrenoHorairId))
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

        // POST: api/CrenoHoraires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CrenoHoraire>> PostCrenoHoraire(CrenoHoraire crenoHoraire)
        {
          if (_context.CrenoHoraires == null)
          {
              return Problem("Entity set 'A21APIContext.CrenoHoraires'  is null.");
          }
            _context.CrenoHoraires.Add(crenoHoraire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrenoHoraire", new { id = crenoHoraire.ID }, crenoHoraire);
        }

        // DELETE: api/CrenoHoraires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrenoHoraire(int id)
        {
            if (_context.CrenoHoraires == null)
            {
                return NotFound();
            }
            var crenoHoraire = await _context.CrenoHoraires.FindAsync(id);
            if (crenoHoraire == null)
            {
                return NotFound();
            }

            _context.CrenoHoraires.Remove(crenoHoraire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CrenoHoraireExists(int id)
        {
            return (_context.CrenoHoraires?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
