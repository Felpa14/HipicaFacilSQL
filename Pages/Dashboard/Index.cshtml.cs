using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly HipicaContext _context;

        public IndexModel(HipicaContext context)
        {
            _context = context;
        }

        public IList<Financa> Financas { get; set; }
        public decimal TotalReceita { get; set; }
        public decimal TotalDespesa { get; set; }
        public decimal MediaTotal { get; set; }

        public async Task OnGetAsync()
        {
            Financas = await _context.Financas.ToListAsync();

            TotalReceita = Financas.Where(f => f.Tipo == "Receita").Sum(f => f.Valor);
            TotalDespesa = Financas.Where(f => f.Tipo == "Despesa").Sum(f => f.Valor);

            MediaTotal = TotalReceita + TotalDespesa;
        }

        public string FinanceChartData()
        {
            var labels = new List<string> { "Receita", "Despesa" };
            var values = new List<decimal> { TotalReceita, TotalDespesa };

            if (TotalDespesa == 0)
            {
                values.Add(0);
            }

            return $"{{\"labels\": {Newtonsoft.Json.JsonConvert.SerializeObject(labels)}, \"values\": {Newtonsoft.Json.JsonConvert.SerializeObject(values)}}}";
        }
    }
}
