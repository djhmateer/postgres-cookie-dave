﻿using System.Security.Claims;
using PostgresCookieDave.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static PostgresCookieDave.Web.Data.CDRole;

namespace PostgresCookieDave.Web.Pages
{
    [AuthorizeRoles(Tier1, Tier2, Admin)]
    public class CrawlModel : PageModel
    {
        public string? Message { get; set; }

        public void OnGet()
        {
            var roleClaims = User.FindAll(ClaimTypes.Role);

            Message = "Role claims are: ";
            foreach (var claim in roleClaims)
            {
                // Tier1, Tier2, Admin etc...
                Message += claim.Value + " ";
            }
        }
    }
}
