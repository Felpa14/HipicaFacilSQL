﻿using System;
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
        public IFormFile ?Imagem { get; set; } // Arquivo de imagem
        public List<IFormFile> ?Documentos { get; set; } // Documentos

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

            if (Documentos != null && Documentos.Count > 0)
            {
                foreach (var doc in Documentos)
                {
                    var docPath = Path.Combine("wwwroot/documents", doc.FileName);
                    using (var stream = new FileStream(docPath, FileMode.Create))
                    {
                        await doc.CopyToAsync(stream);
                    }
                    Cavalo.DocumentosPaths.Add("/documents/" + doc.FileName); // Salvar o caminho do documento
                }
            }

            _context.Cavalos.Add(Cavalo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
