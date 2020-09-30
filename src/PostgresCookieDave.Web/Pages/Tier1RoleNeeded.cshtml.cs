using PostgresCookieDave.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static PostgresCookieDave.Web.Data.CDRole;

namespace PostgresCookieDave.Web.Pages
{
    [AuthorizeRoles(Tier1, Tier2, Admin)]
    public class Tier1RoleNeeded : PageModel
    {
        public void OnGet()
        {
        }
    }
}
