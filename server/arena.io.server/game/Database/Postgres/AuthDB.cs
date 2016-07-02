﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;

namespace arena.Database.Postgres
{
    class AuthDB : IAuthDB
    {
        async void IAuthDB.LoginUser(AuthEntry authEntry, Database.QueryCallback cb)
        {
            using (var conn = new NpgsqlConnection(DatabaseConnectionDefines.PostgresParams))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM users WHERE \"authUserID\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", authEntry.authUserID);
                    var task = cmd.ExecuteReaderAsync();
                    var reader = await task;

                    if (reader != null)
                    {
                        cb(QueryResult.Success, reader);
                    }
                }
            }
        }

        async void IAuthDB.CreateUser(AuthEntry authEntry, Database.QueryCallback cb)
        {
            int result = 0;

            using (var conn = new NpgsqlConnection(DatabaseConnectionDefines.PostgresParams))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("INSERT INTO users (\"authUserID\") VALUES (@id)", conn))
                {
                    cmd.Parameters.AddWithValue("id", authEntry.authUserID);
                    result = await cmd.ExecuteNonQueryAsync();
                }
            }

            if (result == 1)
            {
                (this as IAuthDB).LoginUser(authEntry, cb);
            }
        }
    }
}
