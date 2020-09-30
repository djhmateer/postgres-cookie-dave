using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PostgresCookieDave.Web.Pages
{
    public class ThrowExceptionModel : PageModel
    {
        public void OnGet()
        {
            throw new Exception("user generated exception from code");
        }
    }
}
