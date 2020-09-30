using PostgresCookieDave.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static PostgresCookieDave.Web.Data.CDRole;

namespace PostgresCookieDave.Web.Pages
{
    [AuthorizeRoles(Tier2, Admin)]
    public class Tier2RoleNeeded : PageModel
    {
        public void OnGet()
        {
        }
    }
}
