using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Serilog;

namespace PostgresCookieDave.Web.Pages
{
    public class DBTestModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public string? Message { get; set; }
        public string? Message2 { get; set; }
        public Employee SingleEmployee { get; set; }
        public IEnumerable<Employee> Employees { get; set; }

        public DBTestModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            // works
            //using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=letmein;Database=PostgresCookieDave"))
            //{
            //    connection.Open();
            //    //connection.Execute("Insert into Employee (first_name, last_name, address) values ('John', 'Smith', '123 Duane St');");
            //    var value = connection.Query("Select first_name from Employee;");
            //    Message = $"name in db is: {value.First()}";
            //    //Console.WriteLine(value.First());
            //}

            // doesn't work
            //var db = GetOpenConnection();
            ////var employeex = db.Query<Employee>("Select first_name from Employee");
            //var employeex = db.Query("Select first_name from Employee");
            //var employee = employeex.First();
            //Message2 = $"{employee.first_name} {employee.last_name} {employee.address}";

            // try3 
            //var connectionString = "Host=localhost;Username=postgres;Password=letmein;Database=PostgresCookieDave";

            var connectionString = _configuration.GetConnectionString("Default");

            var employee = await Db.GetSingleEmployee(connectionString);
            SingleEmployee = employee;

            var employees = await Db.GetEmployees(connectionString);
            Employees = employees;
        }

        //public static IDbConnection GetOpenConnection()
        //{
        //    using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=letmein;Database=PostgresCookieDave"))
        //    {
        //        connection.Open();

        //        return connection;
        //    }
        //}
    }

    public static class Db
    {
        public static Task<Employee> GetSingleEmployee(string connectionString)
            => WithConnection(connectionString, async conn =>
                {
                    var result = await conn.QueryAsync<Employee>(
                        "SELECT first_name as FirstName, last_name as LastName, address as Address " +
                        "FROM Employee");

                    return result.FirstOrDefault();
                });

        public static Task<IEnumerable<Employee>> GetEmployees(string connectionString)
            => WithConnection(connectionString, async conn =>
                {
                    var result = await conn.QueryAsync<Employee>(
                        "SELECT first_name as FirstName, last_name as LastName, address as Address " +
                        "FROM Employee");

                    return result;
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

        //private static async Task WithConnection(
        //    string connectionString,
        //    Func<IDbConnection, Task> connectionFunction)
        //{
        //    using (var conn = new NpgsqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        await connectionFunction(conn);
        //    }
        //}

    }

    public class Employee
    {
        // hacking EF
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
