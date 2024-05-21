using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Agenda
{
    public class DeleteModel : PageModel
    {
        private readonly HipicaFacilSQL.Data.HipicaContext _context;

        public DeleteModel(HipicaFacilSQL.Data.HipicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Evento Evento { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Agenda.FirstOrDefaultAsync(m => m.ID == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Agenda.FindAsync(id);
            if (evento != null)
            {
                Evento = evento;
                _context.Agenda.Remove(Evento);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
