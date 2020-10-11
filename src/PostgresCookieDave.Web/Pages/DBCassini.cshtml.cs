using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace PostgresCookieDave.Web.Pages
{
    public class DBCassiniModel : PageModel
    {
        public IList<Thing>? Things { get; set; }

        private readonly IConfiguration _configuration;
        public DBCassiniModel(IConfiguration configuration) => _configuration = configuration;

        public async Task OnGetAsync()
        {
            var connectionString = _configuration.GetConnectionString("Default");

            var things = await Db3.GetThings(connectionString);
            Things = things.ToList();
        }
    }

    public static class Db3
    {
        public static async Task<IEnumerable<Thing>> GetThings(string connectionString)
        {
            //using var db = GetOpenConnection(connectionString);
            using var db = new NpgsqlConnection(connectionString);

            return await db.QueryAsync<Thing>(@"
               SELECT id as Id, date as Date, team as Team, target as Target,
               title as Title, description as Description
               FROM master_plan LIMIT 10");
        }


        public static IDbConnection GetOpenConnection(string connectionString) =>
            new NpgsqlConnection(connectionString);

        //public static IDbConnection GetOpenConnection(string connectionString)
        //{
        //    var connection = new NpgsqlConnection(connectionString);

        //    // dapper opens for us if closed
        //    //connection.Open();

        //    return connection;
        //}


    }

    public static class Db5
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
