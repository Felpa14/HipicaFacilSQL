using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HipicaFacilSQL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : Controller
    {
        private readonly HipicaContext _context;

        public EventosController(HipicaContext context)
        {
            _context = context;
        }

        // GET: api/Eventos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
        {
            return await _context.Agenda.ToListAsync();
        }

        // POST: api/Eventos/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return Ok(new { id = evento.ID });
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Eventos/Delete/{id}
        [HttpDelete("{id}")]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var evento = await _context.Agenda.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            _context.Agenda.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Outros métodos...
    }
}
