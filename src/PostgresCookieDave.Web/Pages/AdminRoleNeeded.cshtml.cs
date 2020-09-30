using Microsoft.AspNetCore.Mvc.RazorPages;
using PostgresCookieDave.Web.Data;
using static PostgresCookieDave.Web.Data.CDRole;

namespace PostgresCookieDave.Web.Pages
{
    [AuthorizeRoles(Admin)]
    public class AdminRoleNeededModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
