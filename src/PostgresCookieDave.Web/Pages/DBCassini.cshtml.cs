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
    public class DBCassiniModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IList<Thing> Things { get; set; }

        public DBCassiniModel(IConfiguration configuration) => _configuration = configuration;

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

            var things= await Db2.GetThings(connectionString);
            Things = things.ToList();
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

    public static class Db2
    {
        public static Task<IEnumerable<Thing>> GetThings(string connectionString)
            => WithConnection(connectionString, async conn =>
                {
                    var result = await conn.QueryAsync<Thing>(
                        "SELECT id as Id, date as Date, team as Team, target as Target, title as Title, description as Description " +
                        "FROM master_plan LIMIT 10");

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
    }

    public class Thing
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Team { get; set; }
        public string Target { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
