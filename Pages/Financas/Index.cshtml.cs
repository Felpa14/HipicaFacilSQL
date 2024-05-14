using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Financas
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
        public string TipoSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Financa> Financas { get; set; }



        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            // using System;
            CurrentSort = sortOrder;
            NomeSort = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            TipoSort = sortOrder == "Tipo" ? "Tipo_desc" : "Tipo";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Financa> financasIQ = from c in _context.Financas
                                             select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                financasIQ = financasIQ.Where(c => c.Nome.Contains(searchString)
                                       || c.Tipo.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nome_desc":
                    financasIQ = financasIQ.OrderByDescending(c => c.Nome);
                    break;
                case "Tipo":
                    financasIQ = financasIQ.OrderBy(c => c.Tipo);
                    break;
                default:
                    financasIQ = financasIQ.OrderBy(c => c.Nome);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Financas = await PaginatedList<Financa>.CreateAsync(
                 financasIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
