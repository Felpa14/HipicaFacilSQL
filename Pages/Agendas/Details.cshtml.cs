using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Agendas
{
    public class DetailsModel : PageModel
    {
        private readonly HipicaFacilSQL.Data.HipicaContext _context;

        public DetailsModel(HipicaFacilSQL.Data.HipicaContext context)
        {
            _context = context;
        }

        public Evento Evento { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FirstOrDefaultAsync(m => m.ID == id);
            if (evento == null)
            {
                return NotFound();
            }
            else
            {
                Evento = evento;
            }
            return Page();
        }
    }
}
