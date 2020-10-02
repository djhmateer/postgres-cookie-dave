using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostgresCookieDave.Web.Pages;

namespace PostgresCookieDave.Web.Data
{
    public class PostgresCookieDaveWebContext : DbContext
    {
        public PostgresCookieDaveWebContext (DbContextOptions<PostgresCookieDaveWebContext> options)
            : base(options)
        {
        }

        public DbSet<PostgresCookieDave.Web.Pages.Employee> Employee { get; set; }
    }
}
