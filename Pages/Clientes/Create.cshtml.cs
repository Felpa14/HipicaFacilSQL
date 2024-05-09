using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HipicaFacilSQL.Data;
using Microsoft.Extensions.Configuration;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Clientes
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
    }
}

