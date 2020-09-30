using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PostgresCookieDave.Web.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        //public string? RequestId { get; set; }

        //public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string? Message { get; set; }

        public void OnGet()
        {
            //RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            Message = exceptionHandlerPathFeature.Error.Message;
            Message += ", " + exceptionHandlerPathFeature.Error.InnerException?.Message;

            //_telemetryClient.TrackException(exceptionHandlerPathFeature.Error);
            //_telemetryClient.TrackEvent("Error.ServerError", new Dictionary<string, string>
            //{
            //    ["originalPath"] = exceptionHandlerPathFeature.Path,
            //    ["error"] = exceptionHandlerPathFeature.Error.Message
            //});
        }

    }
}
