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
using PostgresCookieDave.Web.Services;
using Serilog;

namespace PostgresCookieDave.Web.Pages
{
    public class DBTestModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public string? EnvironmentString { get; set; }
        public string? ConnectionString { get; set; }
        public Employee SingleEmployee { get; set; }
        public IList<Employee> Employees { get; set; }

        public DBTestModel(IConfiguration configuration) => _configuration = configuration;

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

            // environment
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            EnvironmentString = environment;

            var connectionString = _configuration.GetConnectionString("Default");
            ConnectionString = connectionString;

            var employee = await Db.GetSingleEmployee(connectionString);
            SingleEmployee = employee;

            var employees = await Db.GetEmployees(connectionString);
            Employees = employees.ToList();
        }
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
