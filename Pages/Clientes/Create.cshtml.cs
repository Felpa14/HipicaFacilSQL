using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;
using HipicaFacilSQL.Services;

namespace HipicaFacilSQL.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        private readonly HipicaContext _context;
        private readonly ViaCEPService _viaCEPService;

        public CreateModel(HipicaContext context, ViaCEPService viaCEPService)
        {
            _context = context;
            _viaCEPService = viaCEPService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _context.Clientes.Add(Cliente);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Registre ou manipule a exceção conforme necessário.
                ModelState.AddModelError("", "Ocorreu um erro ao salvar os dados do cliente.");
                return Page();
            }
        }

        public async Task<IActionResult> OnGetEnderecoAsync(string cep)
        {
            try
            {
                var endereco = await _viaCEPService.ConsultarCEP(cep);
                return new JsonResult(endereco);
            }
            catch (Exception ex)
            {
                // Registre ou manipule a exceção conforme necessário.
                return NotFound();
            }
        }
    }
}
