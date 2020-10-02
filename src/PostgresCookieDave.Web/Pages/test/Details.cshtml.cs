using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostgresCookieDave.Web.Data;
using PostgresCookieDave.Web.Pages;

namespace PostgresCookieDave.Web.Pages.test
{
    public class DetailsModel : PageModel
    {
        private readonly PostgresCookieDave.Web.Data.PostgresCookieDaveWebContext _context;

        public DetailsModel(PostgresCookieDave.Web.Data.PostgresCookieDaveWebContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);

            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
