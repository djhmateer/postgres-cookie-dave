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
            var connectionString = _configuration.GetConnectionString("Default");


            // 1.works
            //using (var conn = new NpgsqlConnection(connectionString))
            //{
            //    conn.Open();
            //    var result = await conn.QueryAsync<Thing>(
            //        "SELECT id as Id, date as Date, team as Team, target as Target, title as Title, description as Description " +
            //        "FROM master_plan LIMIT 10");
            //    Things = result.ToList();
            //}o

            // 1.5 works.. nice
            //using var db = GetOpenConnection(connectionString);
            //var result = db.Query<Thing>("SELECT id as Id, date as Date, team as Team, target as Target, " +
            //                             "title as Title, description as Description " +
            //                             "FROM master_plan LIMIT 10");
            //Things = result.ToList();

            // 1.7 works and simple
            var things = await Db3.GetThings(connectionString);
            Things = things.ToList();

            // 2. works but clunky (probably use DI to inject in)
            //var conn = new SqlConnectionFactory(connectionString);
            //var db = conn.GetOpenConnection();
            //var result = await db.QueryAsync<Thing>(
            //    "SELECT id as Id, date as Date, team as Team, target as Target, title as Title, description as Description " +
            //    "FROM master_plan LIMIT 10");
            //Things = result.ToList();

            // try3 - works
            //var things = await Db2.GetThings(connectionString);
            //Things = things.ToList();
        }

        //public static IDbConnection GetOpenConnection(string connectionString)
        //{
        //    var connection = new NpgsqlConnection(connectionString);

        //    connection.Open();

        //    return connection;
        //}
        //public class SqlConnectionFactory : IDisposable
        //{
        //    private readonly string _connectionString;
        //    private IDbConnection _connection;

        //    public SqlConnectionFactory(string connectionString)
        //    {
        //        _connectionString = connectionString;
        //    }
        //    public IDbConnection GetOpenConnection()
        //    {
        //        if (_connection == null || _connection.State != ConnectionState.Open)
        //        {
        //            _connection = new NpgsqlConnection(_connectionString);
        //            _connection.Open();
        //        }

        //        return _connection;
        //    }

        //    public void Dispose()
        //    {
        //        if (_connection != null && _connection.State == ConnectionState.Open)
        //        {
        //            _connection.Dispose();
        //        }
        //    }
        //}


    }

    public static class Db3
    {
        public static async Task<IEnumerable<Thing>> GetThings(string connectionString)
        {
            using var db = GetOpenConnection(connectionString);
            var result = await db.QueryAsync<Thing>("SELECT id as Id, date as Date, team as Team, target as Target, " +
                                                    "title as Title, description as Description " +
                                                    "FROM master_plan LIMIT 10");
            return result;
        }


        public static IDbConnection GetOpenConnection(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);

            connection.Open();

            return connection;
        }


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
