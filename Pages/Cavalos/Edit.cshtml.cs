using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Cavalos
{
    public class EditModel : PageModel
    {
        private readonly HipicaFacilSQL.Data.HipicaContext _context;

        public EditModel(HipicaFacilSQL.Data.HipicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cavalo Cavalo { get; set; } = default!;
        public IFormFile? Imagem { get; set; } // Arquivo de imagem

    public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cavalo = await _context.Cavalos.FirstOrDefaultAsync(m => m.ID == id);
            if (cavalo == null)
            {
                return NotFound();
            }
            Cavalo = cavalo;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Imagem != null)
            {
                var filePath = Path.Combine("wwwroot/images", Imagem.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Imagem.CopyToAsync(stream);
                }
                Cavalo.ImagemPath = "/images/" + Imagem.FileName; // Salvar o caminho da imagem
            }

            _context.Attach(Cavalo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CavaloExists(Cavalo.ID))
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

        private bool CavaloExists(int id)
        {
            return _context.Cavalos.Any(e => e.ID == id);
        }
    }
}