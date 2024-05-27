using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;
using System.Threading.Tasks;

namespace HipicaFacilSQL.Pages.Financas
{
    public class EditModel : PageModel
    {
        private readonly HipicaContext _context;

        public EditModel(HipicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Financa Financa { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Financa = await _context.Financas.FirstOrDefaultAsync(m => m.ID == id);

            if (Financa == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Financa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancaExists(Financa.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FinancaExists(int id)
        {
            return _context.Financas.Any(e => e.ID == id);
        }
    }
}
