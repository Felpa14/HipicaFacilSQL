using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Agendas
{
    public class IndexModel : PageModel
    {
        private readonly HipicaFacilSQL.Data.HipicaContext _context;

        public IndexModel(HipicaFacilSQL.Data.HipicaContext context)
        {
            _context = context;
        }

        public IList<Evento> Evento { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Evento = await _context.Eventos.ToListAsync();
        }
    }
}
