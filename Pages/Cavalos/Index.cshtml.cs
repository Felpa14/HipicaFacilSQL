using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Cavalos
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
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string PropSort { get; set; }

        public PaginatedList<Cavalo> Cavalos { get; set; }



        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            // using System;
            CurrentSort = sortOrder;
            NomeSort = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            PropSort = String.IsNullOrEmpty(sortOrder) ? "prop_desc" : "";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Cavalo> cavalosIQ = from c in _context.Cavalos
                                             select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                cavalosIQ = cavalosIQ.Where(c => c.Nome.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nome_desc":
                    cavalosIQ = cavalosIQ.OrderByDescending(c => c.Nome);
                    break; ;
                case "prop_desc":
                    cavalosIQ = cavalosIQ.OrderByDescending(c => c.Proprietario);
                    break; ;
                default:
                    cavalosIQ = cavalosIQ.OrderBy(c => c.Nome);
                    cavalosIQ = cavalosIQ.OrderBy(c => c.Proprietario);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Cavalos = await PaginatedList<Cavalo>.CreateAsync(
                 cavalosIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }

    //    public class IndexModel : PageModel
    //    {
    //        private readonly HipicaFacilSQL.Data.HipicaContext _context;

    //        public IndexModel(HipicaFacilSQL.Data.HipicaContext context)
    //        {
    //            _context = context;
    //        }

    //        public IList<Cavalo> Cavalo { get;set; } = default!;

    //        public async Task OnGetAsync()
    //        {
    //            Cavalo = await _context.Cavalos.ToListAsync();
    //        }
    //    }
}
