using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using PostgresCookieDave.Web.Pages;

namespace PostgresCookieDave.Web.Services
{
    public static class Db
    {
        public static async Task<IEnumerable<Thing>> GetThings(string connectionString)
        {
            using var conn = GetOpenConnection(connectionString);

            var result = await conn.QueryAsync<Thing>(
                @"SELECT id as Id, date as Date, team as Team, target as Target,
                 title as Title, description as Description
                 FROM master_plan LIMIT 10");

            return result;
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



        public static Task<IEnumerable<Thing>> GetThingsFunctional(string connectionString)
            => WithConnection(connectionString, async conn =>
            {
                var result = await conn.QueryAsync<Thing>(
                    "SELECT id as Id, date as Date, team as Team, target as Target, title as Title, description as Description " +
                    "FROM master_plan LIMIT 10");

                return result;
            });


        public static Task<Employee> GetSingleEmployee(string connectionString)
            => WithConnection(connectionString, async conn =>
            {
                var result = await conn.QueryAsync<Employee>(
                    "SELECT first_name as FirstName, last_name as LastName, address as Address " +
                    "FROM employee");

                return result.FirstOrDefault();
            });

        public static Task<IEnumerable<Employee>> GetEmployees(string connectionString)
            => WithConnection(connectionString, async conn =>
            {
                var result = await conn.QueryAsync<Employee>(
                    "SELECT first_name as FirstName, last_name as LastName, address as Address " +
                    "FROM employee");

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
}
