using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Financas
{
    public class DeleteModel : PageModel
    {
        private readonly HipicaFacilSQL.Data.HipicaContext _context;

        public DeleteModel(HipicaFacilSQL.Data.HipicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Financa Financa { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financa = await _context.Financas.FirstOrDefaultAsync(m => m.ID == id);

            if (financa == null)
            {
                return NotFound();
            }
            else
            {
                Financa = financa;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financa = await _context.Financas.FindAsync(id);
            if (financa != null)
            {
                Financa = financa;
                _context.Financas.Remove(Financa);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
