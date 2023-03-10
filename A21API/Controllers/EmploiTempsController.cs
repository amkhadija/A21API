using A21API.Data;
using A21API.Models;
using A21API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace A21API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmploiTempsController : ControllerBase
    {
        private readonly IEmploiTempsService _emploiTempsService;

        public EmploiTempsController(IEmploiTempsService emploiTempsService)
        {
            _emploiTempsService = emploiTempsService;
        }

        // GET: api/EmploiTemps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmploiTemps>>> GetEmploiTemps()
        {
            return Ok(await _emploiTempsService.GetEmploiTemps());
        }

        // GET: api/EmploiTemps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmploiTemps>> GetEmploiTemps(int id)
        {
            var emploiTemps = await _emploiTempsService.GetEmploiTemps(id);
            if (emploiTemps == null)
            {
                return NotFound();
            }
            else
                return Ok(emploiTemps);
        }

        // POST: api/EmploiTemps
        [HttpPost]
        public async Task<ActionResult<EmploiTemps>> PostEmploiTemps(EmploiTemps emploiTemps)
        {
            var result = await _emploiTempsService.SaveEmploiTemps(emploiTemps);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                if (_emploiTempsService.getErrors().Count > 0)
                {
                    return BadRequest(_emploiTempsService.getErrors());
                }
                return Ok(result);
            }
        }

        // DELETE: api/EmploiTemps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploiTemps(int id)
        {
            var result = await _emploiTempsService.DeleteEmploiTemps(id);
            if (result)
            {
                return NoContent();
            }
            else
                return NotFound();
        }
    }
}