using System.Linq;
using System.Security.Claims;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace CookieDave.Web.Pages
{
    public class DBTestModel : PageModel
    {
        public string? Message { get; set; }

        public void OnGet()
        {
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=letmein;Database=PostgresCookieDave"))
            {
                connection.Open();
                connection.Execute("Insert into Employee (first_name, last_name, address) values ('John', 'Smith', '123 Duane St');");
                var value = connection.Query("Select first_name from Employee;");
                Message = $"name in db is: {value.First()}";
                //Console.WriteLine(value.First());
            }

        }
    }
}
