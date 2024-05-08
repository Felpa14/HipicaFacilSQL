﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Financas
{
    public class EditModel : PageModel
    {
        private readonly HipicaFacilSQL.Data.HipicaContext _context;

        public EditModel(HipicaFacilSQL.Data.HipicaContext context)
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

            var financa =  await _context.Financas.FirstOrDefaultAsync(m => m.ID == id);
            if (financa == null)
            {
                return NotFound();
            }
            Financa = financa;
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
