using System;
using System.IO;
using System.Threading.Tasks;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public IFormFile Imagem { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Imagem != null && Imagem.Length > 0)
            {
                // Pasta onde as imagens serão salvas (pode ser ajustada conforme necessário)
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                // Garante que a pasta de uploads exista
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Cria um nome de arquivo único
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Imagem.FileName;

                // Caminho completo para a imagem
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Salva a imagem
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Imagem.CopyToAsync(fileStream);
                }

                // Define o caminho da imagem para exibição na página
                Cavalo.ImagemPath = "/uploads/" + uniqueFileName;
            }

            _context.Cavalos.Add(Cavalo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
