using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace PostgresCookieDave.Web.Pages
{
    public class DBTestModel : PageModel
    {
        public string? Message { get; set; }
        public string? Message2 { get; set; }
        public Employee SingleEmployee { get; set; }

        public async void OnGetAsync()
        {
            // works
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=letmein;Database=PostgresCookieDave"))
            {
                connection.Open();
                //connection.Execute("Insert into Employee (first_name, last_name, address) values ('John', 'Smith', '123 Duane St');");
                var value = connection.Query("Select first_name from Employee;");
                Message = $"name in db is: {value.First()}";
                //Console.WriteLine(value.First());
            }

            // doesn't work
            //var db = GetOpenConnection();
            ////var employeex = db.Query<Employee>("Select first_name from Employee");
            //var employeex = db.Query("Select first_name from Employee");
            //var employee = employeex.First();
            //Message2 = $"{employee.first_name} {employee.last_name} {employee.address}";

            // try3 
            var connectionString = "Host=localhost;Username=postgres;Password=letmein;Database=PostgresCookieDave";
            var employee = await Db.GetEmployee(connectionString);


        }

        public static IDbConnection GetOpenConnection()
        {
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=letmein;Database=PostgresCookieDave"))
            {
                connection.Open();

                return connection;
            }
        }
    }

    public static class Db
    {
        public static Task<Employee> GetEmployee(string connectionString)
            => WithConnection(connectionString, async conn =>
                {
                    var result = await conn.QueryAsync<Employee>(
                        "SELECT first_name as FirstName, last_name as LastName, address as Address " +
                        "FROM Employee");

                    return result.First();
                });

        private static async Task<T> WithConnection<T>(
            string connectionString,
            Func<IDbConnection, Task<T>> connectionFunction)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                return await connectionFunction(conn);
            }
        }

    }



    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
