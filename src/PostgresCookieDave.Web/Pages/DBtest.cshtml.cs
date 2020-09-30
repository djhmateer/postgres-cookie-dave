using System.Data;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using PostgresCookieDave.Services;

namespace CookieDave.Web.Pages
{
    public class DBTestModel : PageModel
    {
        public string? Message { get; set; }
        public string? Message2 { get; set; }

        public void OnGet()
        {
            //using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=letmein;Database=PostgresCookieDave"))
            //{
            //    connection.Open();
            //    connection.Execute("Insert into Employee (first_name, last_name, address) values ('John', 'Smith', '123 Duane St');");
            //    var value = connection.Query("Select first_name from Employee;");
            //    Message = $"name in db is: {value.First()}";
            //    //Console.WriteLine(value.First());
            //}

            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=letmein;Database=PostgresCookieDave"))
            {
                connection.Open();
                //connection.Execute("Insert into Employee (first_name, last_name, address) values ('John', 'Smith', '123 Duane St');");
                var value = connection.Query("Select first_name from Employee;");
                Message = $"name in db is: {value.First()}";
                //Console.WriteLine(value.First());
            }

            var db = GetOpenConnection();
            //var employeex = db.Query<Employee>("Select first_name from Employee");
            var employeex = db.Query("Select first_name from Employee");
            var employee = employeex.First();
            Message2 = $"{employee.first_name} {employee.last_name} {employee.address}";
        }

        public static IDbConnection GetOpenConnection()
        {
            //var connection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=IMDBChallenge;Trusted_Connection=True;MultipleActiveResultSets=true");
            //connection.Open();
            //return connection;

            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=letmein;Database=PostgresCookieDave"))
            {
                connection.Open();

                return connection;
            }
        }
    }

    public class Employee
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
    }
}
