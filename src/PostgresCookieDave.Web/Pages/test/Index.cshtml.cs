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
    public class IndexModel : PageModel
    {
        private readonly PostgresCookieDave.Web.Data.PostgresCookieDaveWebContext _context;

        public IndexModel(PostgresCookieDave.Web.Data.PostgresCookieDaveWebContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; }

        public async Task OnGetAsync()
        {
            Employee = await _context.Employee.ToListAsync();
        }
    }
}
