using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Clientes
{

    public class IndexModel : PageModel
    {
        private readonly HipicaContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(HipicaContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NomeSort { get; set; }
        public string CpfSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Cliente> Clientes { get; set; }



        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            // using System;
            CurrentSort = sortOrder;
            NomeSort = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            CpfSort = sortOrder == "Cpf" ? "Cpf_desc" : "Cpf";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Cliente> clientesIQ = from c in _context.Clientes
                                             select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                clientesIQ = clientesIQ.Where(c => c.Nome.Contains(searchString)
                                       || c.Cpf.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nome_desc":
                    clientesIQ = clientesIQ.OrderByDescending(c => c.Nome);
                    break;
                case "Cpf":
                    clientesIQ = clientesIQ.OrderBy(c => c.Cpf);
                    break;
                default:
                    clientesIQ = clientesIQ.OrderBy(c => c.Nome);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Clientes = await PaginatedList<Cliente>.CreateAsync(
                 clientesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
    //public class IndexModel : PageModel
    //{
    //    private readonly HipicaFacilSQL.Data.HipicaContext _context;

    //    public IndexModel(HipicaFacilSQL.Data.HipicaContext context)
    //    {
    //        _context = context;
    //    }

    //    public IList<Cliente> Cliente { get;set; } = default!;

    //    public async Task OnGetAsync()
    //    {
    //        Cliente = await _context.Clientes.ToListAsync();
    //    }
    //}
}
