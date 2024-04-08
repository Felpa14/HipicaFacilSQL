using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Cavalos
{
    public class CreateModel : PageModel
    {
        private readonly HipicaFacilSQL.Data.HipicaContext _context;

        public CreateModel(HipicaFacilSQL.Data.HipicaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Cavalo Cavalo { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cavalos.Add(Cavalo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
