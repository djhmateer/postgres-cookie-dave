using System.Threading.Tasks;

namespace PostgresCookieDave.Web.Services
{
    public interface IEmailService
    {
        Task SendAsync(string sender, string recipient, string title, string content);
    }
}
