using System;
using System.IO;
using System.Threading.Tasks;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Plugins;

namespace HipicaFacilSQL.Pages.Cavalos
{
    public class CreateModel : PageModel
    {
        private readonly HipicaFacilSQL.Data.HipicaContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(HipicaFacilSQL.Data.HipicaContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Cavalo Cavalo { get; set; }
        [BindProperty]
        public IFormFile Imagem { get; set; } // Arquivo de imagem

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

            _context.Cavalos.Add(Cavalo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
