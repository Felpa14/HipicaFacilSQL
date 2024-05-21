using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HipicaFacilSQL.Pages.Agenda
{
    public class GetEventsModel : PageModel
    {
        private readonly HipicaFacilSQL.Data.HipicaContext _context;

        public GetEventsModel(HipicaFacilSQL.Data.HipicaContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            var events = _context.Agenda.ToList();
            return new JsonResult(events);
        }
    }
}
