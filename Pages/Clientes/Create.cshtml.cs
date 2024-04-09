using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HipicaFacilSQL.Data;
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
        public Cliente Cliente { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyCliente = new Cliente();

            if (await TryUpdateModelAsync<Cliente>(
                emptyCliente,
                "Cliente",   // Prefix for form value.
                c => c.Nome, c => c.Email, c => c.Endereco, c => c.Cpf, c =>c.Cpf, c => c.Telefone))
            {
                _context.Clientes.Add(emptyCliente);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index"); ;
        }
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Clientes.Add(Cliente);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}
    }
}
