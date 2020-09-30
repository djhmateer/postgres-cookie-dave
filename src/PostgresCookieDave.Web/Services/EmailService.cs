using System.Threading.Tasks;
using PostgresCookieDave.Web.Services;

namespace PostgresCookieDave.Web.Services
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(string sender, string recipient, string title, string content)
        {
            // Demoware - not implemented!

            return Task.CompletedTask;
        }
    }
}
