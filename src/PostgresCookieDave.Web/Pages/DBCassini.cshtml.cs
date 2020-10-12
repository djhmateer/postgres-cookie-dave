using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PostgresCookieDave.Web.Services;

namespace PostgresCookieDave.Web.Pages
{
    public class DBCassiniModel : PageModel
    {
        public IList<Thing>? Things { get; set; }

        IConfiguration c;
        public DBCassiniModel(IConfiguration c) => this.c = c;

        public async Task OnGetAsync()
        {
            var connectionString = c.GetConnectionString("Default");

            var things = await Db.GetThings(connectionString);
            Things = things.ToList();
        }
    }

    public class Thing
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Team { get; set; }
        public string Target { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
