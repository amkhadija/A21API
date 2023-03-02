using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A21API.Data;
using A21API.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace A21API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmploiTempsController : ControllerBase
    {
        private readonly A21APIContext _context;

        public EmploiTempsController(A21APIContext context)
        {
            _context = context;
        }

        // GET: api/EmploiTemps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmploiTemps>>> GetEmploiTemps()
        {
          if (_context.EmploiTemps == null)
          {
              return NotFound();
          }
            return await _context.EmploiTemps.ToListAsync();
        }

        // GET: api/EmploiTemps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmploiTemps>> GetEmploiTemps(int id)
        {
          if (_context.EmploiTemps == null)
          {
              return NotFound();
          }
            var emploiTemps = await _context.EmploiTemps.FindAsync(id);

            if (emploiTemps == null)
            {
                return NotFound();
            }

            return emploiTemps;
        }

        //// GET: api/EmploiTemps/CrenoHoraires/5
        //[HttpGet("CrenoHoraires/{emploiTempsId}")]
        //public async Task<ActionResult<IEnumerable<CrenoHoraire>>> GetCrenoHoraires(int emploiTempsId)
        //{
        //    if (_context.EmploiTemps == null)
        //    {
        //        return NotFound();
        //    }
        //    var emploiTemps = await _context.EmploiTemps.FindAsync(emploiTempsId);
        //    if (emploiTemps == null)
        //    {
        //        return NotFound();
        //    }
        //    return emploiTemps.CrenoHoraires.ToList();
        //}

        // PUT: api/EmploiTemps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmploiTemps(int id, EmploiTemps emploiTemps)
        {
            if (id != emploiTemps.ID)
            {
                return BadRequest();
            }

            _context.Entry(emploiTemps).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmploiTempsExists(id))
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

        // POST: api/EmploiTemps
        [HttpPost]
        public async Task<ActionResult<EmploiTemps>> PostEmploiTemps(EmploiTemps emploiTemps)
        {
            if (_context.EmploiTemps == null)
            {
                return Problem("Entity set 'A21APIContext.EmploiTemps' is null.");
            }
            EmploiTemps empt;
            if (emploiTemps.ID == 0) { 
                empt = await CreateEmploiTemps(emploiTemps);
         }
            else
            {
                empt = await UpdateEmploiTemps(emploiTemps);
            }

            if (empt == null)
            {
                return NotFound();
            }


            foreach (var item in empt.CrenoHoraires)
            {
                item.EmploiTemps = null;
                item.Enseignant = null;                
            }
            return Ok(empt);

        }

        private async Task<EmploiTemps> CreateEmploiTemps(EmploiTemps emploiTemps)
        {
            // Create a new EmploiTemps object
            var newEmploiTemps = new EmploiTemps
            {
                Annee_Scolaire = emploiTemps.Annee_Scolaire,
                Nom_Ecole = emploiTemps.Nom_Ecole,
                Groupe = emploiTemps.Groupe,
                Locale = emploiTemps.Locale
            };
            foreach (var ch in emploiTemps.CrenoHoraires)
            {
                ch.ID = 0;
                ch.EmploiTemps = newEmploiTemps;
            }

            newEmploiTemps.CrenoHoraires = emploiTemps.CrenoHoraires;
            // Add the new EmploiTemps object to the context and save changes
            _context.EmploiTemps.Add(newEmploiTemps);
            await _context.SaveChangesAsync();

            return newEmploiTemps;

        }

        public async Task<EmploiTemps> UpdateEmploiTemps(EmploiTemps emploiTemps)
        {
            if (_context.EmploiTemps == null)
            {
                return null;
            }

            var et = _context.EmploiTemps.Find(emploiTemps.ID);
            if (et == null)
                return null;

                et.Locale = emploiTemps.Locale;
                et.Nom_Ecole = emploiTemps.Nom_Ecole;
                et.Annee_Scolaire = emploiTemps.Annee_Scolaire;
                et.Groupe=emploiTemps.Groupe;

            emploiTemps.CrenoHoraires.ToList().ForEach(ch =>
            {
                var item= _context.CrenoHoraires.FirstOrDefault(x => x.ID == ch.ID);
                if(item != null)
                {
                    item.Periode = ch.Periode;
                    item.Jours = ch.Jours;
                    item.EnseignantID = ch.EnseignantID;
                    item.EmploiTempsID=ch.EmploiTempsID;
                }

            });


            await _context.SaveChangesAsync();

            // Use ReferenceHandler.Preserve to handle cycles in the object graph
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return  _context.EmploiTemps.Find(emploiTemps.ID);

        }

        //// POST: api/EmploiTemps
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<EmploiTemps>> PostEmploiTemps(EmploiTemps emploiTemps)
        //{
        //  if (_context.EmploiTemps == null)
        //  {
        //      return Problem("Entity set 'A21APIContext.EmploiTemps'  is null.");
        //  }
        //    _context.EmploiTemps.Add(emploiTemps);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmploiTemps", new { id = emploiTemps.ID }, emploiTemps);
        //}

        // DELETE: api/EmploiTemps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploiTemps(int id)
        {
            if (_context.EmploiTemps == null)
            {
                return NotFound();
            }
            var emploiTemps = await _context.EmploiTemps.FindAsync(id);
            if (emploiTemps == null)
            {
                return NotFound();
            }

            _context.EmploiTemps.Remove(emploiTemps);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmploiTempsExists(int id)
        {
            return (_context.EmploiTemps?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
