using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HipicaFacilSQL.Controllers
{

    public class AgendaController : Controller
    {

        private readonly HipicaContext _context;

        public AgendaController(HipicaContext context)
        {
            _context = context;
        }
    //    [HttpPost]
    //   // POST: api/Eventos
    //    [HttpPost]
    //    public async Task<IActionResult> PostEvento(Evento evento)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        _context.Eventos.Add(evento);
    //        await _context.SaveChangesAsync();

    //        return Ok();
    //    }
    }
}
