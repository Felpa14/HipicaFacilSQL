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
    public class IndexModel : PageModel
    {
        private readonly HipicaFacilSQL.Data.HipicaContext _context;

        public IndexModel(HipicaFacilSQL.Data.HipicaContext context)
        {
            _context = context;
        }

        public IList<Evento> Evento { get; set; } = default!;

        public Evento NovoEvento { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Agenda.Add(NovoEvento);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task OnGetAsync()
        {
            Evento = await _context.Agenda.ToListAsync();
        }
    }
}
