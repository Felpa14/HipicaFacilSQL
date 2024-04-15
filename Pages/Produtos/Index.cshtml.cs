using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Produtos
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

        public PaginatedList<Produto> Produtos { get; set; }


         public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            // using System;
            CurrentSort = sortOrder;
            NomeSort = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Produto> produtosIQ = from c in _context.Produtos
                                             select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                produtosIQ = produtosIQ.Where(c => c.Nome.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nome_desc":
                    produtosIQ = produtosIQ.OrderByDescending(c => c.Nome);
                    break;;
                default:
                    produtosIQ = produtosIQ.OrderBy(c => c.Nome);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Produtos = await PaginatedList<Produto>.CreateAsync(
                 produtosIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
